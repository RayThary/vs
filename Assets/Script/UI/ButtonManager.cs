using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private static ButtonManager instance;
    public static ButtonManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("Button").AddComponent<ButtonManager>();
            return instance;
        }
    }
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

    //play ����
    [SerializeField]
    private Toggle shaking;
    [SerializeField]
    private Toggle auto;

    //graphic ����
    [SerializeField]
    private Dropdown �ػ�;
    private readonly List<Resolution> resolutions = new();
    [SerializeField]
    private Dropdown ������;
    private readonly List<int> frameRate = new();
    [SerializeField]
    private Dropdown ȭ����;
    [SerializeField]
    private Dropdown ��Ƽ���ϸ����;
    [SerializeField]
    private Slider ��ų����;

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

    [SerializeField]
    private Button optionClose;

    //ĳ���� 3�� �ε�
    private Character[] charactors;

    private void Awake()
    {
        instance = this;

        player = GameManager.Instance.GetPlayer;

        //�ػ� �ɼ� �ʱ�ȭ
        �ػ�.options.Clear();
        resolutions.AddRange(Screen.resolutions);
        for (int i = 0; i < resolutions.Count; i++)
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
        for (int i = 0; i < frameRate.Count; i++)
        {
            Dropdown.OptionData data = new()
            {
                text = frameRate[i].ToString()
            };
            ������.options.Add(data);
        }
        ������.RefreshShownValue();

        //ȭ���� �ɼ� �ʱ�ȭ
        ȭ����.options.Clear();
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

        ȭ����.RefreshShownValue();

        ��Ƽ���ϸ����.options.Clear();
        Dropdown.OptionData anti1 = new() { text = "None" };
        Dropdown.OptionData anti2 = new() { text = "FXAA" };
        Dropdown.OptionData anti3 = new() { text = "SMAA" };
        Dropdown.OptionData anti4 = new() { text = "TAA" };

        ��Ƽ���ϸ����.options.Add(anti1);
        ��Ƽ���ϸ����.options.Add(anti2);
        ��Ƽ���ϸ����.options.Add(anti3);
        ��Ƽ���ϸ����.options.Add(anti4);
        ��Ƽ���ϸ����.RefreshShownValue();

        charactors = Resources.LoadAll<Character>("Character");

        SoundManager.instance.SetMasterSound(��ü����);
        SoundManager.instance.SetBGMSound(BGM);
        SoundManager.instance.SetSFXSound(ȿ����);

        optionClose.onClick.AddListener(OnButtonOptionClose);
    }

    public void Refresh()
    {
        shaking.isOn = false;
        auto.isOn = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                �ػ�.value = i;
                break;
            }
        }
        for (int i = 0; i < frameRate.Count; i++)
        {
            if (Application.targetFrameRate == frameRate[i])
            {
                ������.value = i;
                break;
            }
        }
        ȭ����.value = (int)Screen.fullScreenMode;

        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();
        for (int i = 0; i <= 4; i++)
        {
            if (cameraData.antialiasing == (AntialiasingMode)i)
            {
                ��Ƽ���ϸ����.value = i;
                break;
            }
        }
        ��ų����.value = 1;
        ȿ����.value = 0.4f;
    }

    public void Refresh(PlayerSetting playerSetting)
    {
        auto.isOn = playerSetting.Auto;
        shaking.isOn = playerSetting.Shaking;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (playerSetting.Resolution.width == resolutions[i].width && playerSetting.Resolution.height == resolutions[i].height)
            {
                �ػ�.value = i;
            }
        }
        for (int i = 0; i < frameRate.Count; i++)
        {
            if (playerSetting.FrameRate == frameRate[i])
                ������.value = i;
        }
        ȭ����.value = (int)playerSetting.FullScreenMode;
        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();
        ��Ƽ���ϸ����.value = (int)playerSetting.Antialiasing;
        cameraData.antialiasing = playerSetting.Antialiasing;
        ��ų����.value = playerSetting.Transparency;
        ��ü����.value = playerSetting.FullSound * 0.01f;
        BGM.value = playerSetting.FullSound * 0.01f;
        ȿ����.value = playerSetting.SoundEffect * 0.01f;
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
        CharactorSelect.SetActive(true);
        CharactorSelect.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = charactors[0].Sprite;
        CharactorSelect.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = charactors[1].Sprite;
        CharactorSelect.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = charactors[2].Sprite;

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
        ingameOptionWindow.SetActive(false);
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

    public void OnShakingToggle()
    {
        player.Setting.Shaking = shaking.isOn;
    }
    public void OnAutoTargetToggle()
    {
        player.Setting.Auto = auto.isOn;
    }

    public void �ػ󵵼���()
    {
        player.Setting.Resolution = resolutions[�ػ�.value];
    }

    public void �����Ӽ���()
    {
        player.Setting.FrameRate = frameRate[������.value];
    }

    public void ȭ�鼳��()
    {
        player.Setting.FullScreenMode = (FullScreenMode)ȭ����.value;
    }

    public void OnButton��Ƽ���ϸ����()
    {
        player.Setting.Antialiasing = (AntialiasingMode)��Ƽ���ϸ����.value;
    }

    public void OnButton��ų����()
    {
        player.Setting.Transparency = ��ų����.value;
    }

    public void OnSound��ü����()
    {
        player.Setting.FullSound = (int)(��ü����.value * 100);
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

    //esc�� ���������� �۵��ؾ� �ϴϱ�
    public void ESC()
    {
        if (ingameOptionButton.gameObject.activeSelf)
        {
            OnButtonIngameOption();
        }
        else
        {
            OnButtonIngameClose();
        }
    }

    public void OnButtonIngameOption()
    {
        GameManager.Instance.TimeStop = true;
        armory.gameObject.SetActive(false);
        viewArmory.Close();
        ingameOptionButton.gameObject.SetActive(false);
        ingameOptionWindow.SetActive(true);
    }

    public void OnButtonIngameClose()
    {
        GameManager.Instance.TimeStop = false;
        armory.gameObject.SetActive(true);
        ingameOptionButton.gameObject.SetActive(true);
        ingameOptionWindow.SetActive(false);
        damage.Close();
    }

    public void OnButtonMainMenu()
    {
        GameManager.Instance.GameTimer = 0;
        GameManager.Instance.TimeStop = false;
        //�÷��̾� ���� ���� ���־� ��
        //�ʵ� �� ���־� ��
        MenuWindowActive();
        player.Armory.Clear();
        Destroy(player.SelectCharacter.gameObject);
        PoolingManager.Instance.RemoveAllPoolingObject(GameManager.Instance.GetPoolingTemp.gameObject);
        PoolingManager.Instance.RemoveAllPoolingObject(GameManager.Instance.GetEnemyPoolingTemp.gameObject);
        //�ɼ� �ݱ� ��ư�� ���� ó��
        OptionButtonMainMenu();
    }

    public void OnButtonInOption()
    {
        optionWindow.SetActive(false);
        ingameOptionWindow.SetActive(true);
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
        CharactorSelect.SetActive(false);
        InGameWindowActive();
        GameManager.Instance.GetPlayer.SelectCharacter = Instantiate(charactors[id]);
        GameManager.Instance.SetCharactor(GameManager.Instance.GetPlayer.SelectCharacter.transform);
        //�ɼ� �ݱ� ��ư�� ���� ó��
        OptionButtonIngame();
    }

    public void OnButton���ư���()
    {
        CharactorSelect.SetActive(false);
        MenuWindowActive();
    }

    //���θ޴� ����
    public void OptionButtonMainMenu()
    {
        //�ɼ� �ݱ� ��ư�� ���� ó��
        optionClose.onClick.RemoveAllListeners();
        optionClose.onClick.AddListener(OnButtonOptionClose);
    }
    //�ΰ��� ����
    public void OptionButtonIngame()
    {
        //�ɼ� �ݱ� ��ư�� ���� ó��
        optionClose.onClick.RemoveAllListeners();
        optionClose.onClick.AddListener(OnButtonInOption);
    }
}
