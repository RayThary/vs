using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PoolingManager;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public enum Clips
    {
        PlayerHit,
        UnitHit,
    }

    private AudioSource m_backGroundSource;
    //������ͼ��� ��������� �־��ٰ�
    [SerializeField] private AudioMixer m_mixer;
    [SerializeField] private AudioClip m_BackGroundClip;
    [SerializeField] private float m_bgmStartDealy = 0.5f;

    private Transform poolingObjParentTrs;//Ǯ��������Ʈ�� �θ���ġ
    private Transform sfxParent;//�θ���������� �־��ٰ�

    //�����Ŭ���� �� ����ִ� ������ҽ�
    [SerializeField] private GameObject SFXsource;
    //Ŭ������ �־��ְ� �̳ѿ� Ŭ���̶� �̸����Ȱ��� �־��־����


    // Clip : ���� �Ҹ� , ClipCount : �Ҹ��� �ִ��밳��
    [System.Serializable]
    public class PoolingClips
    {
        public AudioClip Clip;
        public int ClipCount;
    }
    [SerializeField] private List<PoolingClips> clips;

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

        //DontDestroyOnLoad(gameObject);
        m_backGroundSource = GetComponent<AudioSource>();

        StartCoroutine(bgStart());
        poolingObjParentTrs = transform.GetChild(0);
        sfxParent = transform.GetChild(1);
        initPoolingClip();
    }

    private void initPoolingClip()
    {
        for (int i = 0; i < clips.Count; i++)
        {
            GameObject sfxParent = new GameObject();

            PoolingClips data = clips[i];
            string sfxName = data.Clip.name;
            int sfxCount = data.ClipCount;

            sfxParent.name = sfxName;
            sfxParent.transform.SetParent(poolingObjParentTrs);


            initPollingChild(sfxCount, sfxParent.transform, data);
        }

    }

    private void initPollingChild(int _count, Transform _parent, PoolingClips _data)
    {
        for (int i = 0; i < _count; i++)
        {
            GameObject sfx = Instantiate(SFXsource);
            sfx.GetComponent<AudioSource>().clip = _data.Clip;
            sfx.name = _data.Clip.name;
            sfx.transform.SetParent(_parent);
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
        string sClip = _clip.ToString();
        Transform parent = poolingObjParentTrs.Find(sClip);
        GameObject sfx = getPoolingObject(parent);

        StartCoroutine(SFXPlaying(sfx.transform, _volum, _SFXTime));
    }
    public void SFXCreate(Clips _clip, Transform _parent)
    {
        string sClip = _clip.ToString();
        Transform parent = poolingObjParentTrs.Find(sClip);
        GameObject sfx = getPoolingObject(parent);
        StartCoroutine(SFXPlaying(sfx.transform, 1, 0));
    }
    public void SFXCreate(Clips _clip)
    {
        string sClip = _clip.ToString();
        Transform parent = poolingObjParentTrs.Find(sClip);
        GameObject sfx = getPoolingObject(parent);
        if (sfx != null)
        {
            sfx.transform.SetParent(sfxParent);
            StartCoroutine(SFXPlaying(sfx.transform, 1, 0));
        }
    }

    IEnumerator SFXPlaying(Transform clip, float _volum, float _SFXTime)
    {


        AudioSource m_sfxaudiosource = clip.GetComponent<AudioSource>();

        m_sfxaudiosource.outputAudioMixerGroup = m_mixer.FindMatchingGroups("SFX")[0];
        m_sfxaudiosource.loop = false;
        m_sfxaudiosource.volume = _volum;
        m_sfxaudiosource.time = _SFXTime;
        m_sfxaudiosource.Play();
        yield return new WaitForSeconds(m_sfxaudiosource.clip.length);
        removePooling(clip.gameObject);
    }

    private GameObject getPoolingObject(Transform _parent)
    {

        GameObject obj = null;
        if (_parent.childCount > 0)
        {
            obj = _parent.GetChild(0).gameObject;
            obj.transform.SetParent(_parent);
            obj.SetActive(true);
        }


        return obj;
    }

    private void removePooling(GameObject _obj)
    {
        string name = _obj.name;
        Transform parent = poolingObjParentTrs.Find(name);

        PoolingClips poolingObj = clips.Find(x => x.Clip.name == name);
        int PoolingCount = poolingObj.ClipCount;
        //������ ���°�� ä���־��ְ� ���� �ڽ��� ������ �����ٸ� �������ش�.
        if (poolingObjParentTrs.childCount < PoolingCount)
        {
            if (_obj == null)
            {
                _obj = Instantiate(SFXsource);
            }

            _obj.transform.SetParent(poolingObjParentTrs.Find(name));
            _obj.SetActive(false);
            _obj.transform.position = Vector3.zero;
            _obj.GetComponent<AudioSource>().time = 0;

        }
        else
        {
            Debug.LogError("�ʰ��ȿ�����Ʈ�� �����Ȱ��");
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
