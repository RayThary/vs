using System.Collections;
using System.Collections.Generic;
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

    //첫 메뉴 인방
    [SerializeField]
    private GameObject baseWindow;
    [SerializeField]
    private GameObject guideWindow;
    [SerializeField]
    private GameObject optionWindow;

    //guideWindow의 하위
    [SerializeField]
    private GameObject playWindow;
    [SerializeField]
    private GameObject displayWindow;
    [SerializeField]
    private GameObject soundWindow;

    //guideWindow의 설정
    [SerializeField]
    private Dropdown 해상도;
    private readonly List<Resolution> resolutions = new();
    [SerializeField]
    private Dropdown 프레임;
    private readonly List<int> frameRate = new();
    [SerializeField]
    private Dropdown 화면모드;
    [SerializeField]
    private Dropdown 안티에일리어싱;
    [SerializeField]
    private Slider 스킬투명도;

    //사운드
    [SerializeField]
    private Slider 전체음성;
    [SerializeField]
    private Slider BGM;
    [SerializeField]
    private Slider 효과음;

    [SerializeField]
    private GameObject CharactorSelect;

    //ingame 메뉴
    [SerializeField]
    private Button armory;
    //armory대신 armoryBack가 꺼지고 켜지고
    [SerializeField]
    private ViewArmory viewArmory;

    [SerializeField]
    private Button ingameOptionButton;
    [SerializeField]
    private GameObject ingameOptionWindow;

    [SerializeField]
    private Damage damage;


    //캐릭터 3개 로딩
    private Character[] charactors;

    private void Awake()
    {
        instance = this;

        player = GameManager.Instance.GetPlayer;

        //해상도 옵션 초기화
        해상도.options.Clear();
        resolutions.AddRange(Screen.resolutions);
        for (int i = 0; i < resolutions.Count; i++)
        {
            Dropdown.OptionData optionData = new()
            {
                text = resolutions[i].width + " X " + resolutions[i].height + " " + (int)resolutions[i].refreshRateRatio.value + "hz"
            };
            해상도.options.Add(optionData);
        }
        해상도.RefreshShownValue();

        //프레임 옵션 초기화
        프레임.options.Clear();
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
            프레임.options.Add(data);
        }
        프레임.RefreshShownValue();

        //화면모드 옵션 초기화
        화면모드.options.Clear();
        Dropdown.OptionData data1 = new()
        {
            text = "전체화면"
        };
        Dropdown.OptionData data2 = new()
        {
            text = "테두리 없는 창모드"
        };
        Dropdown.OptionData data3 = new()
        {
            text = "이건 뭐지"
        };
        Dropdown.OptionData data4 = new()
        {
            text = "창모드"
        };
        화면모드.options.Add(data1);
        화면모드.options.Add(data2);
        화면모드.options.Add(data3);
        화면모드.options.Add(data4);

        화면모드.RefreshShownValue();

        안티에일리어싱.options.Clear();
        Dropdown.OptionData anti1 = new() { text = "None" };
        Dropdown.OptionData anti2 = new() { text = "FXAA" };
        Dropdown.OptionData anti3 = new() { text = "SMAA" };
        Dropdown.OptionData anti4 = new() { text = "TAA" };

        안티에일리어싱.options.Add(anti1);
        안티에일리어싱.options.Add(anti2);
        안티에일리어싱.options.Add(anti3);
        안티에일리어싱.options.Add(anti4);
        안티에일리어싱.RefreshShownValue();

        charactors = Resources.LoadAll<Character>("Character");

    }

    public void Refresh()
    {
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                해상도.value = i;
                break;
            }
        }
        for (int i = 0; i < frameRate.Count; i++)
        {
            if (Application.targetFrameRate == frameRate[i])
            {
                프레임.value = i;
                break;
            }
        }
        화면모드.value = (int)Screen.fullScreenMode;

        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();
        for (int i = 0; i <= 4; i++)
        {
            if (cameraData.antialiasing == (AntialiasingMode)i)
            {
                안티에일리어싱.value = i;
                break;
            }
        }
        스킬투명도.value = 1;
    }

    public void Refresh(PlayerSetting playerSetting)
    {
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (playerSetting.Resolution.width == resolutions[i].width && playerSetting.Resolution.height == resolutions[i].height)
            {
                해상도.value = i;
            }
        }
        for (int i = 0; i < frameRate.Count; i++)
        {
            if (playerSetting.FrameRate == frameRate[i])
                프레임.value = i;
        }
        화면모드.value = (int)playerSetting.FullScreenMode;
        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();
        안티에일리어싱.value = (int)playerSetting.Antialiasing;
        cameraData.antialiasing = playerSetting.Antialiasing;
        스킬투명도.value = playerSetting.Transparency;
    }

    private void MenuWindowOff()
    {
        //첫 메뉴 3인방을 전부 비활성화 해야하지만 사실 시작버튼은 basewindow에만 있고
        //basewindow와 다른 메뉴가 같이 활성화 되어있는 경우는 없으니 basewindow만 비활성화
        baseWindow.SetActive(false);
    }
    //혹시 모르니 함수로 나누자
    private void MenuWindowActive()
    {
        //인게임 메뉴들 비활성화
        InGameMenuOff();

        //태초의 메인메뉴는 basewindow만 활성화 되어있는 상태이니 다른건 활성화할 필요가 없다
        baseWindow.SetActive(true);
    }
    private void InGameMenuOff()
    {
        //인게임 메뉴들 비활성화
        armory.gameObject.SetActive(false);
        ingameOptionButton.gameObject.SetActive(false);
        ingameOptionWindow.SetActive(false);
    }
    private void InGameWindowActive()
    {
        MenuWindowOff();

        //인게임 메뉴 전부 활성화
        armory.gameObject.SetActive(true);
        ingameOptionButton.gameObject.SetActive(true);
    }

    public void OnButton게임시작()
    {
        //캐릭터 선택 후 시작임
        MenuWindowOff();
        CharactorSelect.SetActive(true);
        CharactorSelect.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = charactors[0].Sprite;
        CharactorSelect.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = charactors[1].Sprite;
        CharactorSelect.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = charactors[2].Sprite;

        //InGameWindowActive();
    }

    public void OnButton도감()
    {
        baseWindow.SetActive(false);
        guideWindow.SetActive(true);
    }

    public void OnButton설정()
    {
        baseWindow.SetActive(false);
        optionWindow.SetActive(true);
    }

    public void OnButton종료()
    {
        Application.Quit();
    }

    public void OnButton도감닫기()
    {
        guideWindow.SetActive(false);
        baseWindow.SetActive(true);
    }

    public void OnButton플레이설정()
    {
        playWindow.SetActive(true);
        displayWindow.SetActive(false);
        soundWindow.SetActive(false);
    }

    public void OnButton디스플레이설정()
    {
        playWindow.SetActive(false);
        displayWindow.SetActive(true);
        soundWindow.SetActive(false);
    }

    public void OnButton소리설정()
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

    public void 해상도설정()
    {
        player.Setting.Resolution = resolutions[해상도.value];
        Screen.SetResolution(resolutions[해상도.value].width, resolutions[해상도.value].height, (FullScreenMode)화면모드.value, resolutions[해상도.value].refreshRateRatio);
    }

    public void 프레임설정()
    {
        player.Setting.FrameRate = frameRate[프레임.value];
        Application.targetFrameRate = frameRate[프레임.value];
    }

    public void 화면설정()
    {
        player.Setting.FullScreenMode = (FullScreenMode)화면모드.value;
        Screen.SetResolution(resolutions[해상도.value].width, resolutions[해상도.value].height, (FullScreenMode)화면모드.value, resolutions[해상도.value].refreshRateRatio);
    }

    public void OnButton안티에일리어싱()
    {
        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();
        cameraData.antialiasing = (AntialiasingMode)안티에일리어싱.value;
        player.Setting.Antialiasing = (AntialiasingMode)안티에일리어싱.value;
    }

    public void OnButton스킬투명도()
    {
        player.Setting.Transparency = 스킬투명도.value;
    }

    public void OnSound전체음성()
    {
        player.Setting.FullSound = (int)(전체음성.value * 100);
    }

    public void OnSoundBGM()
    {
        player.Setting.BGM = (int)(BGM.value * 100);
    }

    public void OnSound효과음()
    {
        player.Setting.SoundEffect = (int)(효과음.value * 100);
    }

    public void OnButtonArmory()
    {
        viewArmory.ViewWeapon();
    }

    //esc를 눌렀을때도 작동해야 하니까
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
        //시간 멈춰야 함
        Debug.Log("시간 멈춰야 함");
        GameManager.Instance.TimeScale = 0;
        armory.gameObject.SetActive(false);
        viewArmory.Close();
        ingameOptionButton.gameObject.SetActive(false);
        ingameOptionWindow.SetActive(true);
    }

    public void OnButtonIngameClose()
    {
        //시간 흘러야 함
        Debug.Log("시간 흘러야 함");
        GameManager.Instance.TimeScale = 1;
        armory.gameObject.SetActive(true);
        ingameOptionButton.gameObject.SetActive(true);
        ingameOptionWindow.SetActive(false);
        damage.Close();
    }

    public void OnButtonMainMenu()
    {
        //게임 초기화 해야함
        Debug.Log("게임 초기화 해야함");
        //플레이어 무기 전부 없애야 함
        //필드 몹 없애야 함
        MenuWindowActive();
        player.Armory.Clear();
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
    }

    public void OnButton돌아가기()
    {
        CharactorSelect.SetActive(false);
        MenuWindowActive();
    }
}
