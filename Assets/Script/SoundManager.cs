using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public enum Clips
    {
        Atk_Cavalry,
        Atk_Guard,
        Atk_Magic,
        Atk_Range,
        Atk_Sword,
        Buy,
        Defeat,
        Upgrade,
        Victory,
    }

    private AudioSource m_backGroundSource;
    //������ͼ��� ��������� �־��ٰ�
    [SerializeField] private AudioMixer m_mixer;
    [SerializeField] private AudioClip m_BackGroundClip;
    [SerializeField] private float m_bgmStartDealy = 0.5f;

    private Transform pollingObjParentTrs;//Ǯ��������Ʈ�� �θ���ġ

    //�����Ŭ���� �� ����ִ� ������ҽ�
    [SerializeField] private GameObject SFXsource;
    //Ŭ������ �־��ְ� �̳ѿ� Ŭ���̶� �̸����Ȱ��� �־��־����
    [SerializeField] private List<AudioClip> clips = new List<AudioClip>();
    [SerializeField] private int poolingCount = 50;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        m_backGroundSource = GetComponent<AudioSource>();

        StartCoroutine(bgStart());
        pollingObjParentTrs = transform.GetChild(0);
        initPoolingClip();
    }


    private void initPoolingClip()
    {
        for (int i = 0; poolingCount > i; i++)
        {
            GameObject sfx = Instantiate(SFXsource);
            sfx.transform.SetParent(pollingObjParentTrs);
            sfx.SetActive(false);
        }
    }

    IEnumerator bgStart()
    {
        yield return new WaitForSeconds(m_bgmStartDealy);

        bgSoundPlay(m_BackGroundClip);
    }


    /// <summary>
    /// ���� �̳��� ���ؼ� ����� Ŭ���� ����, ���� ũ�� , ������ �������� �������� ���������� ó�� ������1
    /// </summary>
    /// <param name="_clip">���� �Ҹ�</param>
    /// <param name="_volum">�Ҹ��� ũ��</param>
    /// <param name="_SFXTime">�Ҹ��� ��������</param>
    /// <param name="_parent">������Ʈ�� �θ�</param>
    public void SFXCreate(Clips _clip, float _volum, float _SFXTime, Transform _parent)
    {
        AudioClip clip = clips.Find(x => x.name == _clip.ToString());
        StartCoroutine(SFXPlaying(clip, _volum, _SFXTime, _parent));
    }
    public void SFXCreate(Clips _clip, Transform _parent)
    {
        AudioClip clip = clips.Find(x => x.name == _clip.ToString());
        StartCoroutine(SFXPlaying(clip, 1, 0, _parent));
    }

    IEnumerator SFXPlaying(AudioClip clip, float _volum, float _SFXTime, Transform _parent)
    {
        GameObject SFXSource = getPoolingObject(_parent);

        AudioSource m_sfxaudiosource = SFXSource.GetComponent<AudioSource>();

        m_sfxaudiosource.outputAudioMixerGroup = m_mixer.FindMatchingGroups("SFX")[0];
        m_sfxaudiosource.clip = clip;
        m_sfxaudiosource.loop = false;
        m_sfxaudiosource.volume = _volum;
        m_sfxaudiosource.time = _SFXTime;
        m_sfxaudiosource.Play();
        yield return new WaitForSeconds(clip.length);
        removePooling(SFXSource);
    }

    private GameObject getPoolingObject(Transform _parent)
    {
        int parentCount = pollingObjParentTrs.childCount;
        GameObject obj = null;
        if (parentCount > 0)
        {
            obj = pollingObjParentTrs.GetChild(0).gameObject;

        }
        else
        {
            GameObject sfx = Instantiate(SFXsource);
            sfx.transform.SetParent(pollingObjParentTrs);
            sfx.SetActive(false);
            obj = sfx;
        }

        obj.transform.SetParent(_parent);
        obj.SetActive(true);

        return obj;
    }

    private void removePooling(GameObject _obj)
    {

        //������ ���°�� ä���־��ְ� ���� �ڽ��� ������ �����ٸ� �������ش�.
        if (pollingObjParentTrs.childCount < poolingCount)
        {
            if (_obj == null)
            {
                _obj = Instantiate(SFXsource);
            }

            _obj.transform.SetParent(pollingObjParentTrs);
            _obj.SetActive(false);
            _obj.transform.position = Vector3.zero;
            _obj.GetComponent<AudioSource>().clip = null;

        }
        else
        {
            Destroy(_obj);
        }
    }


    private void bgSoundPlay(AudioClip clip)
    {

        m_backGroundSource.outputAudioMixerGroup = m_mixer.FindMatchingGroups("BackGround")[0];
        m_backGroundSource.clip = clip;
        m_backGroundSource.loop = true;
        m_backGroundSource.time = 0;
        m_backGroundSource.volume = 0.5f;
        m_backGroundSource.Play();
    }

    /// <summary>
    /// ���� ���߱� 
    /// </summary>
    /// <param name="_value"></param>
    public void bgSoundPause(bool _value)
    {
        if (_value)
        {
            m_backGroundSource.Pause();
        }
        else
        {
            m_backGroundSource.time = 0;
            m_backGroundSource.UnPause();
        }
    }


    /// �����̴�����

    public void SetMasterSound(Slider _valeu)
    {
        _valeu.onValueChanged.AddListener(x => m_mixer.SetFloat("Master", Mathf.Log10(x) * 20));
    }

    public void SetSFXSound(Slider _value)
    {
        _value.onValueChanged.AddListener(x => m_mixer.SetFloat("SFX", Mathf.Log10(x) * 20));

    }

    public void SetBGMSound(Slider _value)
    {
        _value.onValueChanged.AddListener(x => m_mixer.SetFloat("BackGround", Mathf.Log10(x) * 20));
    }
}
