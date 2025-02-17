using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//��Ÿ� ���� ���� �����ؼ� ���� ����� ������ �߻��ϴ� ���
public class Magic_9 : IAddon
{
    public string AddonName => "9";

    private readonly Player player;
    //����ü �ӵ�
    private readonly float speed;
    //�����
    private float damage;
    //���� ������
    private readonly float delay;
    //���� ������ ��� Ÿ�̸�
    private float timer;
    //��������Ʈ
    public Sprite Sprite { get => GameManager.Instance.Magic[8]; }

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

    public Magic_9(Player player)
    {
        description = "�Ҳ��� ���� ����� ������ �߻��Ѵ�";
        this.player = player;
        speed = 5;
        damage = 5;
        delay = 1;
        level = 0;
    }
    public void Addon()
    {
        timer = Time.time;
        level = 1;
    }

    //����� ����
    public void LevelUp()
    {
        level++;
        damage += 1f;
        if (level == MaxLevel)
        {
            //���� ¦�̵Ǵ� ��ȭ�� �־�� �� 11���� ��ȣ�� ����
            var power = player.Armory.Addons.OfType<AttackSpeed>().FirstOrDefault();
            if (power != null && power.Level == power.MaxLevel)
            {
                player.Armory.Remove(this);
                player.Armory.Addon(new Magic_11(player));
            }
        }
    }

    public void Remove()
    {
        level = 0;
        damage = 1;
        //��� �߻�ü ����
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));
        projectives.Clear();
        if (coroutine != null)
            GameManager.Instance.StopCoroutine(coroutine);
        coroutine = null;
    }

    private Coroutine coroutine;
    public void Update()
    {
        if ((player.Stat.AttackCount + level * 0.1f) >= timer + (delay - player.Stat.AttackCool))
        {
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
        for (int i = 0; i < player.Stat.AttackCount + 1; i++)
        {
            //������ �����ؾ� ��
            //��� ����
            Vector2 dir = GameManager.Instance.GetTargetTrs.position - player.SelectCharacter.transform.position;
            //���� �������� �־�� ��
            float angle = Vector2.Angle(Vector2.up, dir);
            //ù���� �ٵ� ������ ����� �ҵ�
            if (i != 0)
                angle += Random.Range(-10, 11);

            if (GameManager.Instance.GetTargetTrs.position.x < player.SelectCharacter.transform.position.x)
            {
                angle = -angle;
            }
            //������ vector��
            dir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

            //����ü ����
            Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic9, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
            projective.Init();
            projective.transform.position = player.SelectCharacter.transform.position + (Vector3)dir;
            projective.transform.eulerAngles = new Vector3(0, 0, -angle);
            projective.Attributes.Add(new P_Move(projective, dir, speed));
            projective.Attributes.Add(new P_Damage(this, damage));
            projective.Attributes.Add(new P_DistanceDelete(7, projective.transform.position, projective));

            projectives.Add(projective);

            yield return new WaitForSeconds(0.1f);
        }
        timer = Time.time;
    }

    private IEnumerator ContinuousFire()
    {
        while (true)
        {
            //���� ��ó�� �ִ���
            if (GameManager.Instance.GetTargetTrs.TryGetComponent(out Enemy enemy))
            {
                //������ �����ؾ� ��
                //��� ����
                Vector2 dir = enemy.transform.position - player.SelectCharacter.transform.position;
                //���� �������� �־�� ��
                float angle = Vector2.Angle(Vector2.up, dir);

                if (enemy.transform.position.x < player.SelectCharacter.transform.position.x)
                {
                    angle = -angle;
                }
                //������ vector��
                dir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

                //����ü ����
                Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic9, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
                projective.Init();
                projective.transform.position = player.SelectCharacter.transform.position + (Vector3)dir;
                projective.transform.eulerAngles = new Vector3(0, 0, -angle);
                projective.Attributes.Add(new P_Move(projective, dir, speed));
                projective.Attributes.Add(new P_Damage(this, damage));
                projective.Attributes.Add(new P_DistanceDelete(7, projective.transform.position, projective));

                projectives.Add(projective);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
