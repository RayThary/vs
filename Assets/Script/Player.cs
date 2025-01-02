using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerSetting setting;
    public PlayerSetting Setting { get { return setting; } }

    private int playerExp = 0;
    private int needExp = 100;


    void Start()
    {
        //세이브로드 필요함
        setting = new PlayerSetting();

    }

    // Update is called once per frame
    void Update()
    {
        playerLevelUp();
    }

 

    private void playerLevelUp()
    {
        if (playerExp >= needExp)
        {
            if (playerExp - needExp != 0)
            {
                playerExp = playerExp - needExp;
            }
            else
            {
                playerExp = 0;
            }
            float nextExp = needExp * 1.1f;
            needExp = (int)nextExp;
        }
    }

    public void AddExp(int _exp)
    {
        playerExp++;
    }

}
