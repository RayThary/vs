using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

[System.Serializable]
public class PlayerSetting
{
    //ȭ�� ����
    [SerializeField]
    private bool shaking;
    public bool Shaking { get { return shaking; } set {  shaking = value; } }
    //�ڵ�����
    [SerializeField]
    private bool auto;
    public bool Auto { get { return auto; } set { auto = value; } }

    //���
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private RefreshRate refreshRate; 
    public Resolution Resolution { get { return new Resolution() { width = width, height = height, refreshRateRatio = refreshRate }; } set { width = value.width; height = value.height; refreshRate = value.refreshRateRatio; } }

    //������
    [SerializeField]
    private int frameRate;
    public int FrameRate { get {  return frameRate; } set {  frameRate = value; Application.targetFrameRate = frameRate; } }
    //ȭ����
    [SerializeField]
    private FullScreenMode fullScreenMode;
    public FullScreenMode FullScreenMode { get {  return fullScreenMode; } set {  fullScreenMode = value; Screen.SetResolution(width, height, fullScreenMode, refreshRate); } }
    //��Ƽ���ϸ����
    [SerializeField]
    private AntialiasingMode antialiasing;
    public AntialiasingMode Antialiasing { get {  return antialiasing; } 
        set
        {  
            antialiasing = value; 
            UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();
            cameraData.antialiasing = antialiasing;
        } }

    //��ų ����
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

    //��ü����
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
            //����Ŵ���
        }
    }
    //ȿ����
    [SerializeField]
    private int soundEffect;
    public int SoundEffect 
    {   get {  return soundEffect; } 
        set 
        { 
            soundEffect = value; 
            //����Ŵ���
        }
    }
}
