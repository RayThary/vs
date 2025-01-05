using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCool : IAddon
{
    public string AddonName => "AttackCool";

    private float hap;

    private Sprite sprite;
    public Sprite Sprite => sprite;

    private string description;
    public string Description { get => description; }

    public bool Weapon => false;

    public float Statistics { get => 0; set { } }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public AttackCool()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Weapon & Tool/Arrow");
        description = TableData.Instance.Description.description(AddonName);
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        level = 0;
        GameManager.Instance.GetPlayer.Stat.AttackCool += 0.15f;
        hap += 0.15f;
    }

    public void LevelUp()
    {
        level++;
        GameManager.Instance.GetPlayer.Stat.AttackCool += 0.15f;
        hap += 0.15f;
    }

    public void Remove()
    {
        level = 0;
        GameManager.Instance.GetPlayer.Stat.AttackCool -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
