using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : IAddon
{
    private int hap;

    public Sprite Sprite => null;

    public bool Weapon => false;
    public float Statistics { get => 0; set { } }

    public AttackDamage() 
    {
        hap = 0;
    }

    public void Addon()
    {
        GameManager.Instance.GetPlayer.Stat.AttackDamage += 5;
        hap += 5;
    }

    public void LevelUp()
    {
        GameManager.Instance.GetPlayer.Stat.AttackDamage += 5;
        hap += 5;
    }

    public void Remove()
    {
        GameManager.Instance.GetPlayer.Stat.AttackDamage -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
