using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GraphicSetting : MonoBehaviour
{
    //guideWindow의 설정
    [SerializeField]
    private Dropdown resolution;
    private List<Resolution> resolutions = new();
    public Resolution Defalut;
    [SerializeField]
    private Dropdown frame;
    private readonly List<int> frameRate = new();
    [SerializeField]
    private Dropdown screenMode;
    [SerializeField]
    private Dropdown anti_Aliasing;
    [SerializeField]
    private Slider transparency;

    private void Awake()
    {
        //해상도 옵션 초기화
        resolution.options.Clear();
        // 기존 해상도 리스트 가져오기
        resolutions.AddRange(Screen.resolutions);

        // 중복을 제거하고 가장 높은 refresh rate만 저장할 Dictionary 생성
        Dictionary<string, Resolution> resolutionDict = new();

        foreach (var res in resolutions)
        {
            string key = res.width + "x" + res.height;

            // 만약 같은 해상도가 있다면 refreshRateRatio가 더 높은 경우만 저장
            if (!resolutionDict.ContainsKey(key) || resolutionDict[key].refreshRateRatio.value < res.refreshRateRatio.value)
            {
                resolutionDict[key] = res;
            }
        }

        // Dictionary에서 중복 제거된 해상도 리스트로 변환
        resolutions = resolutionDict.Values.ToList();

        // Dropdown에 추가
        resolution.options.Clear();
        for (int i = 0; i < resolutions.Count; i++)
        {
            Dropdown.OptionData optionData = new()
            {
                text = $"{resolutions[i].width} X {resolutions[i].height} {(int)resolutions[i].refreshRateRatio.value}hz"
            };
            resolution.options.Add(optionData);

            // 현재 화면 해상도와 일치하면 기본 선택값 설정
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                resolution.value = i;
            }
            if (resolutions[i].width == 1920 && resolutions[i].height == 1080)
            {
                Defalut = resolutions[i];
            }
        }
        resolution.RefreshShownValue();

        //프레임 옵션 초기화
        frame.options.Clear();
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
            frame.options.Add(data);
        }
        frame.RefreshShownValue();

        //화면모드 옵션 초기화
        screenMode.options.Clear();
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
        screenMode.options.Add(data1);
        screenMode.options.Add(data2);
        screenMode.options.Add(data3);
        screenMode.options.Add(data4);

        screenMode.RefreshShownValue();

        anti_Aliasing.options.Clear();
        Dropdown.OptionData anti1 = new() { text = "None" };
        Dropdown.OptionData anti2 = new() { text = "FXAA" };
        Dropdown.OptionData anti3 = new() { text = "SMAA" };
        Dropdown.OptionData anti4 = new() { text = "TAA" };

        anti_Aliasing.options.Add(anti1);
        anti_Aliasing.options.Add(anti2);
        anti_Aliasing.options.Add(anti3);
        anti_Aliasing.options.Add(anti4);
        anti_Aliasing.RefreshShownValue();
    }

    public void Refresh()
    {
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                resolution.value = i;
                break;
            }
        }
        for (int i = 0; i < frameRate.Count; i++)
        {
            if (Application.targetFrameRate == frameRate[i])
            {
                frame.value = i;
                break;
            }
        }
        screenMode.value = (int)Screen.fullScreenMode;

        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();
        for (int i = 0; i <= 4; i++)
        {
            if (cameraData.antialiasing == (AntialiasingMode)i)
            {
                anti_Aliasing.value = i;
                break;
            }
        }
        transparency.value = 1;
    }

    public void Refresh(PlayerSetting playerSetting)
    {
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (playerSetting.Resolution.width == resolutions[i].width && playerSetting.Resolution.height == resolutions[i].height)
            {
                resolution.value = i;
            }
        }
        for (int i = 0; i < frameRate.Count; i++)
        {
            if (playerSetting.FrameRate == frameRate[i])
                frame.value = i;
        }
        screenMode.value = (int)playerSetting.FullScreenMode;
        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();
        anti_Aliasing.value = (int)playerSetting.Antialiasing;
        cameraData.antialiasing = playerSetting.Antialiasing;
        transparency.value = playerSetting.Transparency;
    }

    public void 해상도설정()
    {
        GameManager.Instance.GetPlayer.Setting.Resolution = resolutions[resolution.value];
    }

    public void 프레임설정()
    {
        GameManager.Instance.GetPlayer.Setting.FrameRate = frameRate[frame.value];
    }

    public void 화면설정()
    {
        GameManager.Instance.GetPlayer.Setting.FullScreenMode = (FullScreenMode)screenMode.value;
    }

    public void 안티에일리어싱()
    {
        GameManager.Instance.GetPlayer.Setting.Antialiasing = (AntialiasingMode)anti_Aliasing.value;
    }

    public void 스킬투명도()
    {
        GameManager.Instance.GetPlayer.Setting.Transparency = transparency.value;
    }
}
