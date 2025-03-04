using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerSetting setting;
    public PlayerSetting Setting { get { return setting; } }
    private PlayerStat stat;
    public PlayerStat Stat { get { return stat; } }
    private Armory armory;
    public Armory Armory { get { return armory; } }

    [SerializeField] private int playerExp = 8;
    [SerializeField] private int needExp = 10;
    private int basicExp => needExp;

    private Character selectCharacter;
    public Character SelectCharacter { get { return selectCharacter; } set { selectCharacter = value; } }

    [SerializeField]
    private CardSelect cardSelect;
    public CardSelect CardSelect { get {  return cardSelect; } }

    void Start()
    {
        if (LoadSaveManager.Instance.LoadJson(ref setting, "setting"))
        {
            //�ε� ����
            ButtonManager.Instance.Refresh(setting);
        }
        else
        {
            //�ε� ����
            setting = new PlayerSetting();
            ButtonManager.Instance.Refresh();
        }
        
        //���̺�ε� ���ʿ�
        stat = new PlayerStat();
        //���̺�ε� �ʿ�������
        armory = GetComponent<Armory>();
    }


    // Update is called once per frame
    void Update()
    {
        PlayerLevelUp();
    }

 

    private void PlayerLevelUp()
    {
        if (playerExp >= needExp)
        {
            if (playerExp - needExp != 0)
            {
                playerExp -= needExp;
            }
            else
            {
                playerExp = 0;
            }
            double nextExp = basicExp * (Mathf.Pow(GameManager.Instance.GetStageLevel, 2f));
            needExp = (int)nextExp;
            cardSelect.On = true;
        }
    }

    public void AddExp(int _exp)
    {
        playerExp += _exp;
    }

    private void OnApplicationQuit()
    {
        LoadSaveManager.Instance.SaveJson(setting, "setting");
    }
}
