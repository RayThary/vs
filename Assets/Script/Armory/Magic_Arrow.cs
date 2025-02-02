using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

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

    public int MaxLevel => 5;

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
        level++;
        if (level >= 5)
        {

        }
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
            GameManager.Instance.StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        timer = Time.time;
        for (int i = 0; i < player.Stat.AttackCount + level; i++)
        {
            //������ �����ؾ� ��
            //��� ����
            Vector2 dir = GameManager.Instance.GetTargetTrs.position - player.SelectCharacter.transform.position;
            //������ �������� �־�� ��
            float angle = Vector2.Angle(Vector2.up, dir) + Random.Range(-10, 11);
            if (GameManager.Instance.GetTargetTrs.position.x < player.SelectCharacter.transform.position.x)
            {
                angle = -angle;
            }
            //������ vector��
            dir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

            //����ü ����
            Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Arrow, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
            projective.Init();
            projective.transform.position = player.SelectCharacter.transform.position;
            projective.transform.eulerAngles = new Vector3(0, 0, -angle);
            //������
            projective.Attributes.Add(new P_Move(projective, dir, 5));
            //�����ϸ� ��������
            projective.Attributes.Add(new P_DeleteTimer(projective, 10));
            projective.Attributes.Add(new P_Damage(this, damage));

            projectives.Add(projective);
            timer = Time.time;
            yield return new WaitForSeconds(0.1f);
        }
        timer = Time.time;
    }
}
