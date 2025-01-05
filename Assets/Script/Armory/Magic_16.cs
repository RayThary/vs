using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_16 : IAddon
{
    public string AddonName => "16";

    private readonly Player player;
    //�߻��� �߻�ü ����
    private readonly Projective projective;
    //�����
    private readonly float damage;
    //���� ������
    private readonly float delay;
    //���� ������ ��� Ÿ�̸�
    private float timer;
    //��������Ʈ
    public Sprite Sprite { get => GameManager.Instance.Magic[15]; }

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

    public Magic_16(Player player)
    {
        projective = Resources.Load<Projective>("Magic/Magic_16");
        description = "���߷� ���� �����Ѵ�";
        this.player = player;
        damage = 1;
        delay = 5;
        level = 0;
    }

    public void Addon()
    {
        timer = Time.time;
        level = 1;
    }

    public void LevelUp()
    {
        level++;
        if (level == MaxLevel)
        {
            //���� ¦�̵Ǵ� ��ȭ�� �־�� �� 13��
            var power = player.Armory.Addons.OfType<AttackCool>().FirstOrDefault();
            if (power != null && power.Level == power.MaxLevel)
            {
                player.Armory.Remove(this);
                player.Armory.Addon(new Magic_13(player));
            }
        }
    }

    public void Remove()
    {
        //��� �߻�ü ����
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");
        projectives.ForEach(x => x.transform.localScale = new Vector3(1, 1, 1));
        projectives.ForEach(x => Object.Destroy(x.gameObject));

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
        //�߻� ���콺 ��ġ�� ����
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");
        //����ü ����
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();

        projective.transform.position = GameManager.Instance.GetTargetTrs.position;
        projective.Attributes.Add(new P_DamageTimer(damage, 1, this));
        projective.Attributes.Add(new P_DeleteTimer(projective, 3));
        projectives.Add(projective);
    }
}
