using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_10 : IAddon
{
    public string AddonName => "10";

    public Sprite Sprite => GameManager.Instance.Magic[9];

    private readonly string description;
    public string Description => description;

    public bool Weapon => true;

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    private readonly Player player;
    //�߻��� �߻�ü ����
    private readonly Projective projective;

    private readonly List<Projective> projectives = new();

    //��� �̵��ӵ� ����
    private readonly float speed;
    //�����
    private readonly float damage;

    public Magic_10(Player player)
    {
        this.player = player;
        projective = Resources.Load<Projective>("Magic/Magic_10");
        description = "�÷��̾� ������ ���ظ� ������ ��븦 �������� �Ѵ�";
        this.player = player;
        speed = 0.3f;
        damage = 1;
        level = 0;
    }

    public void Addon()
    {
        Fire();
        level = 1;
    }

    public void LevelUp()
    {

    }

    public void Remove()
    {

    }

    public void Update()
    {

    }

    private void Fire()
    {
        //����ü ����
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();

        projective.transform.position = player.transform.position;
        projective.Attributes.Add(new P_Damage(this, damage));
        projective.Attributes.Add(new P_SlowTimer(speed, 1));
        projectives.Add(projective);
    }
}