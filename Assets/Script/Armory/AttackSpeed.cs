using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class AttackSpeed : IAddon
{
    public string AddonName => "AttackSpeed";

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

    public int MaxLevel => 1;

    private int value;

    public AttackSpeed()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Weapon & Tool/Torch");
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        level = 1;
        GameManager.Instance.GetPlayer.Stat.AttackSpeed += value;
        hap += value;
    }

    public void LevelUp()
    {
        level++;
        GameManager.Instance.GetPlayer.Stat.AttackSpeed += value;
        hap += value;
        if (level == MaxLevel)
        {
            //서로 짝이되는 강화가 있어야 함 9 -> 11번 강화된 마법
            Magic_9 magic = GameManager.Instance.GetPlayer.Armory.Addons.OfType<Magic_9>().FirstOrDefault();
            if (magic != null && magic.Level == magic.MaxLevel)
            {
                GameManager.Instance.GetPlayer.Armory.Remove(magic);
                GameManager.Instance.GetPlayer.Armory.Addon(new Magic_11(GameManager.Instance.GetPlayer));
            }
        }
    }

    public void Remove()
    {
        level = 0;
        GameManager.Instance.GetPlayer.Stat.AttackSpeed -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
