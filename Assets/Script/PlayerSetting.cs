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
    private int transparency;
    public int Transparency { get {  return transparency; } set {  transparency = value; } }

    //전체음성
    private int fullvoice;
    public int FullVoice { get {  return fullvoice; } set { fullvoice = value; } }
    //bgm
    private int bgm;
    public int BGM { get {  return bgm; } set {  bgm = value; } }
    //효과음
    private int soundEffect;
    public int SoundEffect { get {  return soundEffect; } set {  soundEffect = value; } }
}
