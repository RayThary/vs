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
    

    private readonly List<Projective> projectives = new();

    //��� �̵��ӵ� ����
    private readonly float speed;
    //�����
    private readonly float damage;

    private readonly float cycle;

    public Magic_19(Player player)
    {
        this.player = player;
        description = "�÷��̾� ������ ���ظ� ������ ��븦 �������� �Ѵ�";
        this.player = player;
        speed = 0.3f;
        damage = 2;
        cycle = 1;
        level = 0;
    }

    public void Update()
    {
        if (projectives.Count > 0 && projectives[0].transform.GetChild(0).TryGetComponent(out Animator component))
        {
            if (component.speed != player.Stat.AttackSpeed)
                component.speed = player.Stat.AttackSpeed;
        }
    }

    public void Addon()
    {
        Fire();
        level = 1;
    }

    public void Remove()
    {
        level = 0;
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));
        projectives.Clear();
    }

    public void LevelUp()
    {
        //��ȭ�� �������� ����
    }

    private void Fire()
    {
        //����ü ����
        
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic19, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
        projective.Init();

        projective.transform.position = player.SelectCharacter.transform.position;
        projective.Attributes.Add(new P_DamageTimer(damage, cycle, this));
        projective.Attributes.Add(new P_Slow(speed));
        projectives.Add(projective);
    }
}
