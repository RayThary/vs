using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : IAddon
{
    public string AddonName => "Speed";

    private float hap;

    private Sprite sprite;
    public Sprite Sprite => sprite;

    public bool Weapon => false;

    public float Statistics { get => 0; set { } }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public Speed()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Equipment/Leather Boot");
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        GameManager.Instance.GetPlayer.Stat.Speed += 1;
        hap += 1;
    }

    public void LevelUp()
    {
        GameManager.Instance.GetPlayer.Stat.Speed += 1;
        hap += 1;
    }

    public void Remove()
    {
        GameManager.Instance.GetPlayer.Stat.Speed -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
