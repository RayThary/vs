using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LifeAbsorption : IAddon
{
    public string AddonName => "LifeAbsorption";

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

    public LifeAbsorption()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Monster Part/Monster Meat");
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        level = 1;
        GameManager.Instance.GetPlayer.Stat.LifeAbsorption += 2;
        hap += 2;
    }

    public void LevelUp()
    {
        level++;
        GameManager.Instance.GetPlayer.Stat.LifeAbsorption += 2;
        hap += 2;
        if (level == MaxLevel)
        {
            //서로 짝이되는 강화가 있어야 함 18 -> +5번 강화된 마법
            Magic_18 magic = GameManager.Instance.GetPlayer.Armory.Addons.OfType<Magic_18>().FirstOrDefault();
            if (magic != null && magic.Level == magic.MaxLevel)
            {
                if (magic.Enhance == false)
                    magic.Enhance = true;
            }
        }
    }

    public void Remove()
    {
        level = 0;
        GameManager.Instance.GetPlayer.Stat.LifeAbsorption -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
