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

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private readonly List<Projective> projectives = new();

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public Magic_8(Player player, float speed, float damage)
    {
        projective = Resources.Load<Projective>("Magic/Magic_8");
        this.player = player;
        this.speed = speed;
        this.damage = damage;
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
    }

    public void LevelUp()
    {
        Fire();
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
        if(projectives.Count - 3 - level != player.Stat.AttackCount)
        {
            Debug.Log("���� �߻�");
            Debug.Log(player.Stat.AttackCount - (projectives.Count - 3 - level));
            Debug.Log(player.Stat.AttackCount);
            Debug.Log(projectives.Count - 3 - level);

            for (int i = 0; i < player.Stat.AttackCount - (projectives.Count - 3 - level); i++)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        //����ü ����
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();
        //���⼭ ������ �޾ƿ�
        Vector2 dir = new(Random.Range(-1, 1f), Random.Range(-1, 1f));

        projective.transform.position = player.transform.position + (Vector3)dir;
        //projective.transform.eulerAngles = new Vector3(0, 0, -angle);
        projective.Attributes.Add(new P_Move(projective, dir, speed));
        projective.Attributes.Add(new P_Bounce(projective, projective.Attributes.OfType<P_Move>().FirstOrDefault(), 1));
        projective.Attributes.Add(new P_Damage(this, damage));
        projectives.Add(projective);
    }
}
