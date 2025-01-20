using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magic_19 : IAddon
{
    public string AddonName => "10";

    public Sprite Sprite => GameManager.Instance.Magic[18];

    private readonly string description;
    public string Description => description;

    public bool Weapon => true;

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    private readonly Player player;
    //�߻��� �߻�ü ����
    private readonly Projective projective;

    private readonly List<Projective> projectives = new();

    //��� �̵��ӵ� ����
    private readonly float speed;
    //�����
    private readonly float damage;

    private readonly float cycle;

    public Magic_19(Player player)
    {
        this.player = player;
        projective = Resources.Load<Projective>("Magic/Magic_19");
        description = "�÷��̾� ������ ���ظ� ������ ��븦 �������� �Ѵ�";
        this.player = player;
        speed = 0.3f;
        damage = 2;
        cycle = 1;
        level = 0;
    }

    public void Update()
    {
        Fire();
        level = 1;
    }

    public void Addon()
    {

    }

    public void Remove()
    {
        level = 0;
        //��� �߻�ü ����
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");
        projectives.ForEach(x => Object.Destroy(x.gameObject));
    }

    public void LevelUp()
    {
        //��ȭ�� �������� ����
    }

    private void Fire()
    {
        //����ü ����
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();

        projective.transform.position = player.SelectCharacter.transform.position;
        projective.Attributes.Add(new P_DamageTimer(damage, cycle, this));
        projective.Attributes.Add(new P_Slow(speed));
        projectives.Add(projective);
    }
}
