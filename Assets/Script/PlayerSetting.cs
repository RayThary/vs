using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting
{
    //ȭ�� ����
    private bool shaking;
    public bool Shaking { get { return shaking; } set {  shaking = value; } }
    //�ڵ�����
    private bool auto;
    public bool Auto { get { return auto; } set { auto = value; } }

    //���
    private Resolution resolution;
    public Resolution Resolution { get {  return resolution; } set { resolution = value; } }
    //������
    private int frameRate;
    public int FrameRate { get {  return frameRate; } set {  frameRate = value; } }
    //ȭ����
    private FullScreenMode fullScreenMode;
    public FullScreenMode FullScreenMode { get {  return fullScreenMode; } set {  fullScreenMode = value; } }
    //��Ƽ���ϸ����

    //��ų ����
    private int transparency;
    public int Transparency { get {  return transparency; } set {  transparency = value; } }

    //��ü����
    private int fullvoice;
    public int FullVoice { get {  return fullvoice; } set { fullvoice = value; } }
    //bgm
    private int bgm;
    public int BGM { get {  return bgm; } set {  bgm = value; } }
    //ȿ����
    private int soundEffect;
    public int SoundEffect { get {  return soundEffect; } set {  soundEffect = value; } }
}
