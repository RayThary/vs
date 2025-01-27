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

    private readonly List<Projective> projectives = new();

    //��� �̵��ӵ� ����
    private float speed;
    //�����
    private readonly float damage;

    public Magic_10(Player player)
    {
        this.player = player;
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
        level++;
        speed += 0.1f;
        if (level == MaxLevel)
        {
            //���� ¦�̵Ǵ� ��ȭ�� �־�� �� 19
            var power = player.Armory.Addons.OfType<ShieldPoint>().FirstOrDefault();
            if (power != null && power.Level == power.MaxLevel)
            {
                player.Armory.Remove(this);
                player.Armory.Addon(new Magic_19(player));
            }
        }
    }

    public void Remove()
    {
        level = 0;
        speed = 0.3f;
        //��� �߻�ü ����
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));
        projectives.Clear();
    }

    public void Update()
    {

    }

    private void Fire()
    {
        //����ü ����
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic10, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
        projective.Init();

        projective.transform.position = player.SelectCharacter.transform.position;
        projective.Attributes.Add(new P_Damage(this, damage));
        projective.Attributes.Add(new P_SlowTimer(speed, 1));
        projectives.Add(projective);
    }
}
