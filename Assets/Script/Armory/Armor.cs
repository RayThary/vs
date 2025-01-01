using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : IAddon
{
    public string AddonName => "Armor";

    private float hap;

    private Sprite sprite;
    public Sprite Sprite => sprite;

    public bool Weapon => false;

    public float Statistics { get => 0; set { } }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public Armor()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Equipment/Iron Armor");
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        GameManager.Instance.GetPlayer.Stat.Armor += 1;
        hap += 1;
    }

    public void LevelUp()
    {
        GameManager.Instance.GetPlayer.Stat.Armor += 1;
        hap += 1;
    }

    public void Remove()
    {
        GameManager.Instance.GetPlayer.Stat.Armor -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
