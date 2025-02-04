using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magic_Bullet : IAddon
{
    public string AddonName => "Bullet";

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

    public Magic_Bullet(Player player)
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
        if(coroutine != null)
            GameManager.Instance.StopCoroutine(coroutine);
        coroutine = null;
    }

    private Coroutine coroutine;
    public void Update()
    {
        if((player.Stat.AttackCount + level * 0.1f) >= timer + (delay - player.Stat.AttackCool))
        {
            Debug.Log(player.Stat.AttackCount + level * 0.1f);
            Debug.Log(timer + (delay - player.Stat.AttackCool));
            if (coroutine == null)
                coroutine = GameManager.Instance.StartCoroutine(ContinuousFire());
        }
        else
        {
            //���� �����̰� �Ǿ�����
            if (timer + (delay - player.Stat.AttackCool) <= Time.time)
            {
                GameManager.Instance.StartCoroutine(Fire());
            }
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
            //Projective projective = Object.Instantiate(Resources.Load<Projective>("Magic/BulletA"));
            projective.Init();
            projective.transform.position = player.SelectCharacter.transform.position;
            projective.transform.eulerAngles = new Vector3(0, 0, -angle + 90);
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

    private IEnumerator ContinuousFire()
    {
        while (true)
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
            //Projective projective = Object.Instantiate(Resources.Load<Projective>("Magic/BulletA"));
            projective.Init();
            projective.transform.position = player.SelectCharacter.transform.position;
            projective.transform.eulerAngles = new Vector3(0, 0, -angle + 90);
            //������
            projective.Attributes.Add(new P_Move(projective, dir, 5));
            //�����ϸ� ��������
            projective.Attributes.Add(new P_DeleteTimer(projective, 10));
            projective.Attributes.Add(new P_Damage(this, damage));

            projectives.Add(projective);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
