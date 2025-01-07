using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShieldPoint : IAddon
{
    public string AddonName => "ShieldPoint";

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

    public int MaxLevel => 2;

    public ShieldPoint()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Equipment/Helm");
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        level = 0;
        GameManager.Instance.GetPlayer.Stat.ShieldPoint += 25;
        hap += 25;
    }

    public void LevelUp()
    {
        level++;
        GameManager.Instance.GetPlayer.Stat.ShieldPoint += 25;
        hap += 25;
        if (level == MaxLevel)
        {
            //서로 짝이되는 강화가 있어야 함 10 -> 19번 강화된 마법
            Magic_10 magic = GameManager.Instance.GetPlayer.Armory.Addons.OfType<Magic_10>().FirstOrDefault();
            if (magic != null && magic.Level == magic.MaxLevel)
            {
                GameManager.Instance.GetPlayer.Armory.Remove(magic);
                GameManager.Instance.GetPlayer.Armory.Addon(new Magic_19(GameManager.Instance.GetPlayer));
            }
        }
    }

    public void Remove()
    {
        level = 0;
        GameManager.Instance.GetPlayer.Stat.ShieldPoint -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
