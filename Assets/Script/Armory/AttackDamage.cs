using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : IAddon
{
    public string AddonName => "AttackDamage";

    private int hap;

    private Sprite sprite;
    public Sprite Sprite => sprite;

    private string description;
    public string Description
    {
        get { 
            if(description == null)
                description = TableData.Instance.Description.description(AddonName);
            return description;
        } 
    }

    public bool Weapon => false;
    public float Statistics { get => 0; set { } }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 10;

    public AttackDamage()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Weapon & Tool/Axe");
        hap = 0;
        level = 0;
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
