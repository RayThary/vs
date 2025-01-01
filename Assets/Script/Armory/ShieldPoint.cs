using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPoint : IAddon
{
    public string AddonName => "ShieldPoint";

    private float hap;

    private Sprite sprite;
    public Sprite Sprite => sprite;

    public bool Weapon => false;

    public float Statistics { get => 0; set { } }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 2;

    public ShieldPoint()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Equipment/Helm");
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        GameManager.Instance.GetPlayer.Stat.ShieldPoint += 25;
        hap += 25;
    }

    public void LevelUp()
    {
        GameManager.Instance.GetPlayer.Stat.ShieldPoint += 25;
        hap += 25;
    }

    public void Remove()
    {
        GameManager.Instance.GetPlayer.Stat.ShieldPoint -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
