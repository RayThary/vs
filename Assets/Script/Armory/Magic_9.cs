using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//��Ÿ� ���� ���� �����ؼ� ���� ����� ������ �߻��ϴ� ���
public class Magic_9 : IAddon
{
    private readonly Player player;
    //�߻��� �߻�ü ����
    private readonly Projective projective;
    //��Ÿ�
    private readonly float range;
    //����ü �ӵ�
    private readonly float speed;
    //�����
    private readonly float damage;
    //���� ������
    private readonly float delay;
    //���� ������ ��� Ÿ�̸�
    private float timer;
    //��������Ʈ
    public Sprite Sprite {  get => GameManager.Instance.magic[8]; }
    //����
    private float statistics;
    public float Statistics { get { return statistics; } set { statistics = value; } }

    private List<Projective> projectives = new();

    public Magic_9(Player player, float range, float speed, float damage, float delay)
    {
        projective = Resources.Load<Projective>("Magic_9");
        this.player = player;
        this.range = range;
        this.speed = speed;
        this.damage = damage;
        this.delay = delay;
    }

    public void Addon()
    {
        timer = Time.time;
    }

    public void LevelUp()
    {

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
        //���� �����̰� �Ǿ�����
        if(timer + delay <= Time.time)
        {
            //���� ��ó�� �ִ���
            Enemy enemy = DistanceMin(range);
            if (enemy != null)
            {
                //������ �����ؾ� ��
                //��� ����
                Vector2 dir = enemy.transform.position - player.transform.position;
                //����
                float angle = Vector2.Angle(Vector2.up, dir);
                if (enemy.transform.position.x < player.transform.position.x)
                {
                    angle = -angle;
                }
                //������ vector��
                dir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

                timer = Time.time;

                Debug.Log("������ƮǮ���� ����ؾ� �ϴ� ����");
                //����ü ����
                Projective projective = Object.Instantiate(this.projective);
                projective.transform.position = player.transform.position + (Vector3)dir;
                projective.transform.eulerAngles = new Vector3(0, 0, -angle);
                projective.Attributes.Add(new P_Move(projective, dir, speed));
                projective.Attributes.Add(new P_Damage(this, damage));
                //projective.Attributes.Add(new P_EnterDelete(projective));
                projective.Attributes.Add(new P_DistanceDelete(7, projective.transform.position, projective));

                projectives.Add(projective);
            }
        }
    }

    //range�ȿ� ���� ����� ��
    public Enemy DistanceMin(float range)
    {
        Enemy enemy = Enemy.enemyList
            .OrderBy(enemy => Vector3.Distance(player.transform.position, enemy.transform.position)) // �Ÿ� �������� ����
            .FirstOrDefault(); // ���� ����� �� ��ȯ (����Ʈ�� ��������� null ��ȯ)
        if (enemy == null)
            return null;
        else if(Vector2.Distance(enemy.transform.position, player.transform.position) < range)
            return enemy;
        return null;
    }
}
