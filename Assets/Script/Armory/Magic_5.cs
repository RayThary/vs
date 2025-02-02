using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//18�� ��ȭ�� magic5 ��ü�� �ᵵ ��ũ��Ʈ�� �Ⱦ�����
public class Magic_5 : IAddon
{
    public string AddonName => "5";

    //private readonly Player player;
    //�����
    //private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Magic[4];

    private readonly string description;
    public string Description { get => description; }

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private readonly List<Projective> projectives = new();

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    //public Magic_5(Player player)
    //{
    //    description = "�ڽŰ� ���� ������ ���������� ���ظ� ������.";
    //    this.player = player;
    //    damage = 1;
    //    level = 0;
    //}

    public void Addon()
    {
        level = 1;
    }

    public void LevelUp()
    {
        //��ȭ�� �������� ����
    }

    public void Remove()
    {
        level = 0;
        //��� �߻�ü ����
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));
        projectives.Clear();
    }

    public void Update()
    {

    }

    //private void Fire(int angle)
    //{
        
    //}
}
