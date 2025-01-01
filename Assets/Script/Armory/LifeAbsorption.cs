using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeAbsorption : IAddon
{
    public string AddonName => "LifeAbsorption";

    private float hap;

    private Sprite sprite;
    public Sprite Sprite => sprite;

    public bool Weapon => false;

    public float Statistics { get => 0; set { } }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public LifeAbsorption()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Monster Part/Monster Meat");
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        GameManager.Instance.GetPlayer.Stat.LifeAbsorption += 2;
        hap += 2;
    }

    public void LevelUp()
    {
        GameManager.Instance.GetPlayer.Stat.LifeAbsorption += 2;
        hap += 2;
    }

    public void Remove()
    {
        GameManager.Instance.GetPlayer.Stat.LifeAbsorption -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
