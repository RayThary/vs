using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : IAddon
{
    public string AddonName => "HP";

    private float hap;

    private Sprite sprite;
    public Sprite Sprite => sprite;

    private string description;
    public string Description
    {
        get
        {
            if (description == null)
                description = TableData.Instance.Description.description(AddonName);
            return description;
        }
    }

    public bool Weapon => false;

    public float Statistics { get => 0; set { } }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public HP()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Misc/Heart");
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        level = 1;
        GameManager.Instance.GetPlayer.Stat.HP += 3;
        hap += 3;
    }

    public void LevelUp()
    {
        level++;
        GameManager.Instance.GetPlayer.Stat.HP += 3;
        hap += 3;
    }

    public void Remove()
    {
        level = 0;
        GameManager.Instance.GetPlayer.Stat.HP -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
