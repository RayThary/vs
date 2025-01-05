using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : IAddon
{
    public string AddonName => "SkillDamage";

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

    public SkillDamage()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Misc/Scroll");
        description = TableData.Instance.Description.description(AddonName);
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        level = 1;
        GameManager.Instance.GetPlayer.Stat.SkillDamage += 1;
        hap += 1;
    }

    public void LevelUp()
    {
        level++;
        GameManager.Instance.GetPlayer.Stat.SkillDamage += 1;
        hap += 1;
    }

    public void Remove()
    {
        level = 0;
        GameManager.Instance.GetPlayer.Stat.SkillDamage -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
