using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

[System.Serializable]
public class PlayerSetting
{
    //화면 떨림
    [SerializeField]
    private bool shaking;
    public bool Shaking { get { return shaking; } set {  shaking = value; } }
    //자동공격
    [SerializeField]
    private bool auto;
    public bool Auto { get { return auto; } set { auto = value; } }

    //행상도
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private RefreshRate refreshRate; 
    public Resolution Resolution { get { return new Resolution() { width = width, height = height, refreshRateRatio = refreshRate }; } set { width = value.width; height = value.height; refreshRate = value.refreshRateRatio; } }

    //프레임
    [SerializeField]
    private int frameRate;
    public int FrameRate { get {  return frameRate; } set {  frameRate = value; Application.targetFrameRate = frameRate; } }
    //화면모드
    [SerializeField]
    private FullScreenMode fullScreenMode;
    public FullScreenMode FullScreenMode { get {  return fullScreenMode; } set {  fullScreenMode = value; Screen.SetResolution(width, height, fullScreenMode, refreshRate); } }
    //안티에일리어싱
    [SerializeField]
    private AntialiasingMode antialiasing;
    public AntialiasingMode Antialiasing { get {  return antialiasing; } 
        set
        {  
            antialiasing = value; 
            UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();
            cameraData.antialiasing = antialiasing;
        } }

    //스킬 투명도
    [SerializeField]
    private float transparency;
    public float Transparency 
    {   get {  return transparency; } 
        set 
        {  
            transparency = value;
            GameManager.Instance.Material.SetFloat("_Alpha", transparency);
        } 
    }

    //전체음성
    [SerializeField]
    private int fullSound;
    public int FullSound 
    {   get {  return fullSound; } 
        set
        {
            fullSound = value; 
            //SoundManager.instance.SetMasterSound()
        }
    }
    //bgm
    [SerializeField]
    private int bgm;
    public int BGM 
    {   get {  return bgm; } 
        set 
        {  
            bgm = value; 
            //사운드매니져
        }
    }
    //효과음
    [SerializeField]
    private int soundEffect;
    public int SoundEffect 
    {   get {  return soundEffect; } 
        set 
        { 
            soundEffect = value; 
            //사운드매니져
        }
    }
}
