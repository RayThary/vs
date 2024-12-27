using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting
{
    //화면 떨림
    private bool shaking;
    public bool Shaking { get { return shaking; } set {  shaking = value; } }
    //자동공격
    private bool auto;
    public bool Auto { get { return auto; } set { auto = value; } }

    //행상도
    private Resolution resolution;
    public Resolution Resolution { get {  return resolution; } set { resolution = value; } }
    //프레임
    private int frameRate;
    public int FrameRate { get {  return frameRate; } set {  frameRate = value; } }
    //화면모드
    private FullScreenMode fullScreenMode;
    public FullScreenMode FullScreenMode { get {  return fullScreenMode; } set {  fullScreenMode = value; } }
    //안티에일리어싱

    //스킬 투명도
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
