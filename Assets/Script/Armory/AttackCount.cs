using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackCount : IAddon
{
    public string AddonName => "AttackCount";

    private int hap;

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

    public AttackCount()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Weapon & Tool/Bow");
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        level = 1;
        GameManager.Instance.GetPlayer.Stat.AttackCount += 1;
        hap += 1;
    }

    public void LevelUp()
    {
        level++;
        GameManager.Instance.GetPlayer.Stat.AttackCount += 1;
        hap += 1;
        if (level == MaxLevel)
        {
            //서로 짝이되는 강화가 있어야 함 15 -> 17번 강화된 마법
            Magic_15 magic = GameManager.Instance.GetPlayer.Armory.Addons.OfType<Magic_15>().FirstOrDefault();
            if (magic != null && magic.Level == magic.MaxLevel)
            {
                GameManager.Instance.GetPlayer.Armory.Remove(magic);
                GameManager.Instance.GetPlayer.Armory.Addon(new Magic_17(GameManager.Instance.GetPlayer));
            }
        }
    }

    public void Remove()
    {
        level = 0;
        GameManager.Instance.GetPlayer.Stat.AttackCount -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
