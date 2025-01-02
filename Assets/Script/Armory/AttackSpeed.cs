using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : IAddon
{
    public string AddonName => "AttackSpeed";

    private float hap;

    private Sprite sprite;
    public Sprite Sprite => sprite;

    private string description;
    public string Description { get => description; }

    public bool Weapon => false;

    public float Statistics { get => 0; set { } }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    public AttackSpeed()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Weapon & Tool/Torch");
        description = TableData.Instance.Description.description(AddonName);
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        GameManager.Instance.GetPlayer.Stat.AttackSpeed += 2;
        hap += 2;
    }

    public void LevelUp()
    {
        GameManager.Instance.GetPlayer.Stat.AttackSpeed += 2;
        hap += 2;
    }

    public void Remove()
    {
        GameManager.Instance.GetPlayer.Stat.AttackSpeed -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
