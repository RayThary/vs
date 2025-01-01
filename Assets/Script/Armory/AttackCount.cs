using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCount : IAddon
{
    public string AddonName => "AttackCount";

    private float hap;

    private Sprite sprite;
    public Sprite Sprite => sprite;

    public bool Weapon => false;

    public float Statistics { get => 0; set { } }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public AttackCount()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Weapon & Tool/Bow");
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        GameManager.Instance.GetPlayer.Stat.AttackCount += 1;
        hap += 1;
    }

    public void LevelUp()
    {
        GameManager.Instance.GetPlayer.Stat.AttackCount += 1;
        hap += 1;
    }

    public void Remove()
    {
        GameManager.Instance.GetPlayer.Stat.AttackCount -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
