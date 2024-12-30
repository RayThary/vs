using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_15 : IAddon
{
    private readonly Player player;
    //�߻��� �߻�ü ����
    private readonly Projective projective;
    //����ü �ӵ�
    private readonly float speed;
    //�����
    private readonly float damage;
    //��������Ʈ
    public Sprite Sprite { get => GameManager.Instance.Magic[14]; }
    public bool Weapon => true;

    //����
    private float statistics;
    public float Statistics { get { return statistics; } set { statistics = value; } }

    private List<Projective> projectives = new();

    public Magic_15(Player player, float speed, float damage)
    {
        projective = Resources.Load<Projective>("Magic/Magic_15");
        this.player = player;
        this.speed = speed;
        this.damage = damage;
    }

    public void Addon()
    {
        //�߻� ���콺 ��ġ�� ���� ����� ��
        Debug.Log("������ƮǮ���� ����ؾ� �ϴ� ����");
        //����ü ����
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();
        //���⼭ ������ �޾ƿ�
        Vector2 dir = Vector2.right;
        
        projective.transform.position = player.transform.position + (Vector3)dir;
        //projective.transform.eulerAngles = new Vector3(0, 0, -angle);
        projective.Attributes.Add(new P_Move(projective, dir, speed));
        projective.Attributes.Add(new P_Bounce(projective, projective.Attributes.OfType<P_Move>().FirstOrDefault(), 1));
        projective.Attributes.Add(new P_Damage(this, damage));
        projectives.Add(projective);
    }

    public void LevelUp()
    {
        //ũ�� Ŀ����
        projectives.ForEach(x => x.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f));
        projectives.ForEach(x => x.Attributes.OfType<P_Bounce>().FirstOrDefault().Size += 0.5f);
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
        //�ѹ��� �����ϴ� �Ŵϱ�
    }
}
