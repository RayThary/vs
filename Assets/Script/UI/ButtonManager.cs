using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
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
    private readonly List<Resolution> resolutions = new ();
    [SerializeField]
    private Dropdown 프레임;
    private readonly List<int> frameRate = new();
    [SerializeField]
    private Dropdown 화면모드;

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
    [SerializeField]
    private Charactor[] charactors;
    [SerializeField]
    private Image[] charactorIcons;

    private void Start()
    {
        //해상도 옵션 초기화
        해상도.options.Clear();
        resolutions.AddRange(Screen.resolutions);
        for(int i = 0; i < resolutions.Count; i++)
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
        for(int i = 0; i < frameRate.Count; i++)
        {
            Dropdown.OptionData data = new()
            {
                text = frameRate[i].ToString()
            };
            프레임.options.Add(data);
        }
        프레임.RefreshShownValue() ;

        //화면모드 옵션 초기화
        화면모드.options.Clear ();
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

        화면모드.RefreshShownValue() ;
        Refresh();


        charactors = Resources.LoadAll<Charactor>("Charactor");

    }

    public void Refresh()
    {
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                해상도.value = i;
            }
        }
        for (int i = 0; i < frameRate.Count; i++)
        {
            if (Application.targetFrameRate == frameRate[i])
                프레임.value = i;
        }
        화면모드.value = (int)Screen.fullScreenMode;
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
        CharactorSelect.gameObject.SetActive(true);

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

    public void 안티에일리어싱()
    {
        
    }

    public void OnSound전체음성()
    {
        player.Setting.FullVoice = (int)(전체음성.value * 100);
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

    public void OnButtonIngameOption()
    {
        //시간 멈춰야 함
        Debug.Log("시간 멈춰야 함");
        armory.gameObject.SetActive(false);
        viewArmory.Close();
        ingameOptionButton.gameObject.SetActive(false);
        ingameOptionWindow.SetActive(true);
    }

    public void OnButtonIngameClose()
    {
        //시간 멈춰야 함
        Debug.Log("시간 흘러야 함");
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
        Debug.Log(charactors[id] + " 선택");
        CharactorSelect.SetActive(false);
        InGameWindowActive();
    }
}
