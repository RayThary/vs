using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Boom : IAddon
{
    public string AddonName => "Boom";

    private readonly Player player;
    private readonly List<Projective> projectives = new();


    public Sprite Sprite => GameManager.Instance.Magic[2];

    private readonly string description;
    public string Description { get => description; }

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    //���� ������
    private readonly float delay;

    //���� ������ ��� Ÿ�̸�
    private float timer;
    public Magic_Boom(Player player)
    {
        description = "������ ��ź�� ������";
        this.player = player;
        level = 0;
        delay = 3;
    }

    public void Addon()
    {
        level = 1;
    }

    public void LevelUp()
    {

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
        //���� �����̰� �Ǿ�����
        if (timer + (delay - player.Stat.AttackCool) <= Time.time)
        {
            
            timer = Time.time;
        }
    }

    
}
