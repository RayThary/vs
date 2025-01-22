using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_11 : IAddon
{
    //9�� ��ȭ
    public string AddonName => "11";

    public Sprite Sprite => GameManager.Instance.Magic[10];

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

    //����ü �ӵ�
    private readonly float speed;
    //�����
    private readonly float damage;
    //���� ������
    private readonly float delay;

    //���� ������ ��� Ÿ�̸�
    private float timer;

    public Magic_11(Player player)
    {
        description = "�Ҳ��� ���� ����� ������ �߻��Ѵ�";
        this.player = player;
        speed = 5;
        damage = 4;
        delay = 1;
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
        //���� ��ó�� �ִ���
        if(GameManager.Instance.GetTargetTrs.TryGetComponent(out Enemy enemy))
        {
            //������ �����ؾ� ��
            //��� ����
            Vector2 dir = enemy.transform.position - player.SelectCharacter.transform.position;
            //����
            float angle = Vector2.Angle(Vector2.up, dir);
            if (enemy.transform.position.x < player.SelectCharacter.transform.position.x)
            {
                angle = -angle;
            }
            //������ vector��
            dir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

            //����ü ����
            Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic_11, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
            projective.Init();
            projective.transform.position = player.SelectCharacter.transform.position + (Vector3)dir;
            projective.transform.eulerAngles = new Vector3(0, 0, -angle);
            projective.Attributes.Add(new P_Move(projective, dir, speed));
            projective.Attributes.Add(new P_Damage(this, damage));
            projective.Attributes.Add(new P_DistanceDelete(7, projective.transform.position, projective));

            projectives.Add(projective);
        }
    }
}
