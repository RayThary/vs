using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_8 : IAddon
{
    public string AddonName => "8";

    private readonly Player player;
    //�߻��� �߻�ü ����
    private readonly Projective projective;
    //����ü �ӵ�
    private readonly float speed;
    //�����
    private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Magic[7];

    private readonly string description;
    public string Description { get => description; }

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private readonly List<Projective> projectives = new();

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public Magic_8(Player player)
    {
        projective = Resources.Load<Projective>("Magic/Magic_8");
        description = "���� ƨ��� ��ü�� 3�� �߻��Ѵ�";
        this.player = player;
        speed = 5;
        damage = 1;
        level = 0;
    }

    public void Addon()
    {
        //������ ��ġ
        Debug.Log("������ƮǮ���� ����ؾ� �ϴ� ����");

        for(int i = 0; i < 3; i++)
        {
            Fire();
        }
        level = 1;
    }

    public void LevelUp()
    {
        Fire(); 
        level++;
        if (level == MaxLevel)
        {
            //���� ¦�̵Ǵ� ��ȭ�� �־�� ��
        }
    }

    public void Remove()
    {
        //��� �߻�ü ����
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");
        projectives.ForEach(x => Object.Destroy(x.gameObject));
        projectives.Clear();
    }

    public void Update()
    {
        //�⺻ 3�� + ���� 1 == �⺻ 0 �̵Ƿ��� 2�� ���ؾ� ��
        if(projectives.Count - 2 - level != player.Stat.AttackCount)
        {
            for (int i = 0; i < player.Stat.AttackCount - (projectives.Count - 3 - level); i++)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        //����ü ����
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();
        //���⼭ ������ �޾ƿ�
        Vector2 dir = new(Random.Range(-1, 1f), Random.Range(-1, 1f));

        projective.transform.position = player.SelectCharacter.transform.position + (Vector3)dir;
        projective.Attributes.Add(new P_Move(projective, dir, speed));
        projective.Attributes.Add(new P_Bounce(projective, projective.Attributes.OfType<P_Move>().FirstOrDefault(), 1));
        projective.Attributes.Add(new P_Damage(this, damage));
        projectives.Add(projective);
    }
}
