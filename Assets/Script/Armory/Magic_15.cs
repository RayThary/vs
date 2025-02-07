using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_15 : IAddon
{
    public string AddonName => "15";

    private readonly Player player;
    //����ü �ӵ�
    private readonly float speed;
    //�����
    private readonly float damage;
    //��������Ʈ
    public Sprite Sprite { get => GameManager.Instance.Magic[14]; }

    private readonly string description;
    public string Description { get => description; }

    public bool Weapon => true;

    //����
    private float statistics;
    public float Statistics { get { return statistics; } set { statistics = value; } }

    private readonly List<Projective> projectives = new();

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public Magic_15(Player player)
    {
        description = "���� ƨ��� ū ��ü�� �ϳ��� �߻��Ѵ�";
        this.player = player;
        speed = 3;
        damage = 8;
        level = 0;
    }

    public void Addon()
    {
        Fire();
        level = 1;
    }

    //ũ�� Ŀ����
    public void LevelUp()
    {
        projectives.ForEach(x => x.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f));
        projectives.ForEach(x => x.Attributes.OfType<P_Bounce>().FirstOrDefault().Size += 0.5f);
        level++;
        if (level == MaxLevel)
        {
            //���� ¦�̵Ǵ� ��ȭ�� �־�� �� 17���� ��ȭ�� ����
            var power = player.Armory.Addons.OfType<AttackCount>().FirstOrDefault();
            if (power != null && power.Level == power.MaxLevel)
            {
                player.Armory.Remove(this);
                player.Armory.Addon(new Magic_17(player));
            }
        }
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
        if (projectives.Count - level != player.Stat.AttackCount)
        {
            for (int i = 0; i < player.Stat.AttackCount - (projectives.Count - level); i++)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        //�߻� ���콺 ��ġ�� ���� ����� ��
        //����ü ����
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic15, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
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
