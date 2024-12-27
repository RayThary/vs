using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_8 : IAddon
{
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

    private List<Projective> projectives = new();

    public Magic_8(Player player, float speed, float damage)
    {
        projective = Resources.Load<Projective>("Magic_8");
        this.player = player;
        this.speed = speed;
        this.damage = damage;
    }

    public void Addon()
    {
        //������ ��ġ
        Debug.Log("������ƮǮ���� ����ؾ� �ϴ� ����");

        for(int i = 0; i < 3; i++)
        {
            //����ü ����
            Projective projective = Object.Instantiate(this.projective);
            projective.Init();
            //���⼭ ������ �޾ƿ�
            Vector2 dir = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f));

            projective.transform.position = player.transform.position + (Vector3)dir;
            //projective.transform.eulerAngles = new Vector3(0, 0, -angle);
            projective.Attributes.Add(new P_Move(projective, dir, speed));
            projective.Attributes.Add(new P_Bounce(projective, projective.Attributes.OfType<P_Move>().FirstOrDefault(), 1));
            projective.Attributes.Add(new P_Damage(this, damage));
            projectives.Add(projective);
        }
    }

    public void LevelUp()
    {
        //����ü ����
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();
        //���⼭ ������ �޾ƿ�
        Vector2 dir = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f));

        projective.transform.position = player.transform.position + (Vector3)dir;
        //projective.transform.eulerAngles = new Vector3(0, 0, -angle);
        projective.Attributes.Add(new P_Move(projective, dir, speed));
        projective.Attributes.Add(new P_Bounce(projective, projective.Attributes.OfType<P_Move>().FirstOrDefault(), 1));
        projective.Attributes.Add(new P_Damage(this, damage));
        projectives.Add(projective);
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

    }
}
