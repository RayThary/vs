using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_13 : IAddon
{
    public string AddonName => "13";

    private readonly Player player;
    //�����
    private readonly float damage;
    //���� ������
    private readonly float delay;
    //���� ������ ��� Ÿ�̸�
    private float timer;
    //��������Ʈ
    public Sprite Sprite { get => GameManager.Instance.Magic[12]; }

    private readonly string description;
    public string Description { get => description; }

    public bool Weapon => true;

    //����
    private float statistics;
    public float Statistics { get { return statistics; } set { statistics = value; } }

    private readonly List<Projective> projectives = new();

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    public Magic_13(Player player)
    {
        description = "���߷� ���� �����Ѵ�";
        this.player = player;
        damage = 3;
        delay = 3;
        level = 0;
    }

    public void Addon()
    {
        timer = Time.time;
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
        projectives.ForEach(x => x.transform.localScale = new Vector3(1, 1, 1));
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));

        projectives.Clear();
    }

    public void Update()
    {
        //���� �����̰� �Ǿ�����
        if (timer + (delay - player.Stat.AttackCool) <= Time.time)
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
        if (GameManager.Instance.GetTargetTrs == null)
            return;

        //�߻� ���콺 ��ġ�� ����
        //����ü ����
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic13, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
        projective.Init();

        projective.transform.position = GameManager.Instance.GetTargetTrs.position;
        projective.Attributes.Add(new P_DamageTimer(damage, 1, this));
        projective.Attributes.Add(new P_DeleteTimer(projective, 3));
        projectives.Add(projective);
    }
}
