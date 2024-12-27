using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerSetting setting;
    public PlayerSetting Setting { get { return setting; } }
    private PlayerStat stat;
    public PlayerStat Stat { get { return stat; } }

    [SerializeField] private int playerExp = 8;
    [SerializeField] private int needExp = 10;

    [SerializeField] private Character selectCharacter;
    public Character SelectCharacter { get { return selectCharacter; } }

    void Start()
    {
        //���̺�ε� �ʿ���
        setting = new PlayerSetting();
        //���̺�ε� ���ʿ�
        stat = new PlayerStat();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddExp(int _exp)
    {
        playerExp++;
    }
}
