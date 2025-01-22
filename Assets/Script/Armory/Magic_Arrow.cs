using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Arrow : IAddon
{
    public string AddonName => "Arrow";

    private readonly Player player;
    private readonly List<Projective> projectives = new();

    private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Arrow;

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

    public Magic_Arrow(Player player)
    {
        description = "������ ȭ���� �߻��Ѵ�";
        this.player = player;
        level = 0;
        damage = 1;
        delay = 1;
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
        if (timer + delay <= Time.time)
        {
            for (int i = 0; i < player.Stat.AttackCount + 1; i++)
            {
                Fire();
            }
            timer = Time.time;
        }
    }

    private void Fire()
    {
        //����ü ����
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Arrow, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
        projective.Init();

        projective.transform.position = player.SelectCharacter.transform.position;
        //������
        projective.Attributes.Add(new P_Move(projective, GameManager.Instance.GetTargetTrs.position, 5));
        //�����ϸ� ��������
        projective.Attributes.Add(new P_DeleteTimer(projective, 10));
        projective.Attributes.Add(new P_Damage(this, damage));

        projectives.Add(projective);
    }
}
