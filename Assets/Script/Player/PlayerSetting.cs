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
    private Resolution resolution;
    public Resolution Resolution { get {  return resolution; } set { resolution = value; } }
    //������
    [SerializeField]
    private int frameRate;
    public int FrameRate { get {  return frameRate; } set {  frameRate = value; } }
    //ȭ����
    [SerializeField]
    private FullScreenMode fullScreenMode;
    public FullScreenMode FullScreenMode { get {  return fullScreenMode; } set {  fullScreenMode = value; } }
    //��Ƽ���ϸ����
    [SerializeField]
    private AntialiasingMode antialiasing;
    public AntialiasingMode Antialiasing { get {  return antialiasing; } set {  antialiasing = value; } }

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
