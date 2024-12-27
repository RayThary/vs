using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    //ù �޴� �ι�
    [SerializeField]
    private GameObject baseWindow;
    [SerializeField]
    private GameObject guideWindow;
    [SerializeField]
    private GameObject optionWindow;

    //guideWindow�� ����
    [SerializeField]
    private GameObject playWindow;
    [SerializeField]
    private GameObject displayWindow;
    [SerializeField]
    private GameObject soundWindow;

    //guideWindow�� ����
    [SerializeField]
    private Dropdown �ػ�;
    private readonly List<Resolution> resolutions = new ();
    [SerializeField]
    private Dropdown ������;
    private readonly List<int> frameRate = new();
    [SerializeField]
    private Dropdown ȭ����;

    //����
    [SerializeField]
    private Slider ��ü����;
    [SerializeField]
    private Slider BGM;
    [SerializeField]
    private Slider ȿ����;

    [SerializeField]
    private GameObject CharactorSelect;

    //ingame �޴�
    [SerializeField]
    private Button armory;
    //armory��� armoryBack�� ������ ������
    [SerializeField]
    private ViewArmory viewArmory;

    [SerializeField]
    private Button ingameOptionButton;
    [SerializeField]
    private GameObject ingameOptionWindow;

    [SerializeField]
    private Damage damage;


    //ĳ���� 3�� �ε�
    [SerializeField]
    private Charactor[] charactors;
    [SerializeField]
    private Image[] charactorIcons;

    private void Start()
    {
        //�ػ� �ɼ� �ʱ�ȭ
        �ػ�.options.Clear();
        resolutions.AddRange(Screen.resolutions);
        for(int i = 0; i < resolutions.Count; i++)
        {
            Dropdown.OptionData optionData = new()
            {
                text = resolutions[i].width + " X " + resolutions[i].height + " " + (int)resolutions[i].refreshRateRatio.value + "hz"
            };
            �ػ�.options.Add(optionData);
        }
        �ػ�.RefreshShownValue();

        //������ �ɼ� �ʱ�ȭ
        ������.options.Clear();
        if (Application.targetFrameRate > 240)
            Application.targetFrameRate = 240;
        frameRate.Add(240);
        frameRate.Add(120);
        frameRate.Add(60);
        frameRate.Add(30);
        for(int i = 0; i < frameRate.Count; i++)
        {
            Dropdown.OptionData data = new()
            {
                text = frameRate[i].ToString()
            };
            ������.options.Add(data);
        }
        ������.RefreshShownValue() ;

        //ȭ���� �ɼ� �ʱ�ȭ
        ȭ����.options.Clear ();
        Dropdown.OptionData data1 = new()
        {
            text = "��üȭ��"
        };
        Dropdown.OptionData data2 = new()
        {
            text = "�׵θ� ���� â���"
        };
        Dropdown.OptionData data3 = new()
        {
            text = "�̰� ����"
        };
        Dropdown.OptionData data4 = new()
        {
            text = "â���"
        };
        ȭ����.options.Add(data1);
        ȭ����.options.Add(data2);
        ȭ����.options.Add(data3);
        ȭ����.options.Add(data4);

        ȭ����.RefreshShownValue() ;
        Refresh();


        charactors = Resources.LoadAll<Charactor>("Charactor");

    }

    public void Refresh()
    {
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                �ػ�.value = i;
            }
        }
        for (int i = 0; i < frameRate.Count; i++)
        {
            if (Application.targetFrameRate == frameRate[i])
                ������.value = i;
        }
        ȭ����.value = (int)Screen.fullScreenMode;
    }

    private void MenuWindowOff()
    {
        //ù �޴� 3�ι��� ���� ��Ȱ��ȭ �ؾ������� ��� ���۹�ư�� basewindow���� �ְ�
        //basewindow�� �ٸ� �޴��� ���� Ȱ��ȭ �Ǿ��ִ� ���� ������ basewindow�� ��Ȱ��ȭ
        baseWindow.SetActive(false);
    }
    //Ȥ�� �𸣴� �Լ��� ������
    private void MenuWindowActive()
    {
        //�ΰ��� �޴��� ��Ȱ��ȭ
        InGameMenuOff();

        //������ ���θ޴��� basewindow�� Ȱ��ȭ �Ǿ��ִ� �����̴� �ٸ��� Ȱ��ȭ�� �ʿ䰡 ����
        baseWindow.SetActive(true);
    }
    private void InGameMenuOff()
    {
        //�ΰ��� �޴��� ��Ȱ��ȭ
        armory.gameObject.SetActive(false);
        ingameOptionButton.gameObject.SetActive(false);
        ingameOptionWindow.SetActive(false);
    }
    private void InGameWindowActive()
    {
        MenuWindowOff();

        //�ΰ��� �޴� ���� Ȱ��ȭ
        armory.gameObject.SetActive(true);
        ingameOptionButton.gameObject.SetActive(true);
    }

    public void OnButton���ӽ���()
    {
        //ĳ���� ���� �� ������
        MenuWindowOff();
        CharactorSelect.gameObject.SetActive(true);

        //InGameWindowActive();
    }

    public void OnButton����()
    {
        baseWindow.SetActive(false);
        guideWindow.SetActive(true);
    }

    public void OnButton����()
    {
        baseWindow.SetActive(false);
        optionWindow.SetActive(true);
    }

    public void OnButton����()
    {
        Application.Quit();
    }

    public void OnButton�����ݱ�()
    {
        guideWindow.SetActive(false);
        baseWindow.SetActive(true);
    }

    public void OnButton�÷��̼���()
    {
        playWindow.SetActive(true);
        displayWindow.SetActive(false);
        soundWindow.SetActive(false);
    }

    public void OnButton���÷��̼���()
    {
        playWindow.SetActive(false);
        displayWindow.SetActive(true);
        soundWindow.SetActive(false);
    }

    public void OnButton�Ҹ�����()
    {
        playWindow.SetActive(false);
        displayWindow.SetActive(false);
        soundWindow.SetActive(true);
    }

    public void OnButtonOptionClose()
    {
        optionWindow.SetActive(false);
        baseWindow.SetActive(true);
    }

    public void �ػ󵵼���()
    {
        player.Setting.Resolution = resolutions[�ػ�.value];
        Screen.SetResolution(resolutions[�ػ�.value].width, resolutions[�ػ�.value].height, (FullScreenMode)ȭ����.value, resolutions[�ػ�.value].refreshRateRatio);
    }

    public void �����Ӽ���()
    {
        player.Setting.FrameRate = frameRate[������.value];
        Application.targetFrameRate = frameRate[������.value]; 
    }

    public void ȭ�鼳��()
    {
        player.Setting.FullScreenMode = (FullScreenMode)ȭ����.value;
        Screen.SetResolution(resolutions[�ػ�.value].width, resolutions[�ػ�.value].height, (FullScreenMode)ȭ����.value, resolutions[�ػ�.value].refreshRateRatio);
    }

    public void ��Ƽ���ϸ����()
    {
        
    }

    public void OnSound��ü����()
    {
        player.Setting.FullVoice = (int)(��ü����.value * 100);
    }

    public void OnSoundBGM()
    {
        player.Setting.BGM = (int)(BGM.value * 100);
    }

    public void OnSoundȿ����()
    {
        player.Setting.SoundEffect = (int)(ȿ����.value * 100);
    }

    public void OnButtonArmory()
    {
        viewArmory.ViewWeapon();
    }

    public void OnButtonIngameOption()
    {
        //�ð� ����� ��
        Debug.Log("�ð� ����� ��");
        armory.gameObject.SetActive(false);
        viewArmory.Close();
        ingameOptionButton.gameObject.SetActive(false);
        ingameOptionWindow.SetActive(true);
    }

    public void OnButtonIngameClose()
    {
        //�ð� ����� ��
        Debug.Log("�ð� �귯�� ��");
        armory.gameObject.SetActive(true);
        ingameOptionButton.gameObject.SetActive(true);
        ingameOptionWindow.SetActive(false);
        damage.Close();
    }

    public void OnButtonMainMenu()
    {
        //���� �ʱ�ȭ �ؾ���
        Debug.Log("���� �ʱ�ȭ �ؾ���");
        //�÷��̾� ���� ���� ���־� ��
        //�ʵ� �� ���־� ��
        MenuWindowActive();
    }

    public void OnButtonDamage()
    {
        damage.Open();
    }
    public void OnButtonDamageClose()
    {
        damage.Close();
    }



    public void OnButtonSelect(int id)
    {
        Debug.Log(charactors[id] + " ����");
        CharactorSelect.SetActive(false);
        InGameWindowActive();
    }
}