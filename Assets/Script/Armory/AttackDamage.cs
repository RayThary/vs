using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackDamage : IAddon
{
    public string AddonName => "AttackDamage";

    private int hap;

    private readonly Sprite sprite;
    public Sprite Sprite => sprite;

    private string description;
    public string Description
    {
        get { 
            if(description == null)
                description = TableData.Instance.Description.description(AddonName);
            return description;
        } 
    }

    public bool Weapon => false;
    public float Statistics { get => 0; set { } }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 10;

    public AttackDamage()
    {
        sprite = Resources.Load<Sprite>("Cainos/Pixel Art Icon Pack - RPG/Texture/Weapon & Tool/Axe");
        hap = 0;
        level = 0;
    }

    public void Addon()
    {
        level = 1;
        GameManager.Instance.GetPlayer.Stat.AttackDamage += 5;
        hap += 5;
    }

    public void LevelUp()
    {
        level++;
        GameManager.Instance.GetPlayer.Stat.AttackDamage += 5;
        hap += 5;
        if(level == MaxLevel)
        {
            //���� ¦�̵Ǵ� ��ȭ�� �־�� �� 6 -> 7�� ��ȭ�� ����
            Magic_6 magic = GameManager.Instance.GetPlayer.Armory.Addons.OfType<Magic_6>().FirstOrDefault();
            if (magic != null && magic.Level == magic.MaxLevel)
            {
                GameManager.Instance.GetPlayer.Armory.Remove(magic);
                GameManager.Instance.GetPlayer.Armory.Addon(new Magic_7(GameManager.Instance.GetPlayer));
            }
        }
    }

    public void Remove()
    {
        level = 0;
        GameManager.Instance.GetPlayer.Stat.AttackDamage -= hap;
        hap = 0;
    }

    public void Update()
    {

    }
}
