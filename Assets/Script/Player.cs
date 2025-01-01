using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerSetting setting;
    public PlayerSetting Setting { get { return setting; } }
    private PlayerStat stat;
    public PlayerStat Stat { get { return stat; } }
    private Armory armory;
    public Armory Armory { get {  return armory; } }

    [SerializeField] private int level = 0;
    [SerializeField] private int playerExp = 8;
    [SerializeField] private int needExp = 10;

    [SerializeField] private Character selectCharacter;
    public Character SelectCharacter { get { return selectCharacter; } set { selectCharacter = value; } }

    [SerializeField]
    private CardSelect cardSelect;

    void Start()
    {
        //if(LoadSaveManager.Instance.Load(ref setting, "setting"))
        //{
        //    //로드 성공
        //}
        //else
        //{
        //    //로드 실패
            setting = new PlayerSetting();
        //}
        
        //세이브로드 불필요
        stat = new PlayerStat();
        //세이브로드 필요할지도
        armory = GetComponent<Armory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddExp(int _exp)
    {
        playerExp += _exp;
        if(playerExp >= needExp)
        {
            level++;
            playerExp -= needExp;
            needExp = TableData.Instance.UserLevelTable.Table[level].exp;
            cardSelect.On = true;
        }
    }

    private void OnApplicationQuit()
    {
        //LoadSaveManager.Instance.Save(setting, "setting");
    }
}
