using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Boom : IAddon
{
    public string AddonName => "Boom";

    private readonly Player player;
    //�߻��� �߻�ü ����
    private readonly Projective projective;
    private readonly List<Projective> projectives = new();

    private readonly float damage;

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

    private Projective boom;
    public Magic_Boom(Player player)
    {
        projective = Resources.Load<Projective>("Magic/Magic_3");
        boom = Resources.Load<Projective>("Magic/Magic_20");
        description = "������ ��ź�� ������";
        this.player = player;
        level = 0;
        damage = 2;
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
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");
        projectives.ForEach(x => Object.Destroy(x.gameObject));
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
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();

        projective.transform.position = player.SelectCharacter.transform.position;
        //Ÿ���� �������� ���  //�����ϸ� ����
        projective.Attributes.Add(new P_CircularDestroy(projective, GameManager.Instance.GetTargetTrs.position, 90));
        //�����ϸ� ��������
        projective.Attributes.Add(new P_DestroySpawn(projective, boom));
        projective.Attributes.Add(new P_Damage(this, damage)); 

        projectives.Add(projective);
    }
}
