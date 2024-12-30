using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_6 : IAddon
{
    private readonly Player player;
    //�߻��� �߻�ü ����
    private readonly Projective projective;
    //�����
    private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Magic[5];

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private List<Projective> projectives = new();

    public Magic_6(Player player, float damage)
    {
        projective = Resources.Load<Projective>("Magic/Magic_6");
        this.player = player;
        this.damage = damage;
    }

    public void Addon()
    {
        Debug.Log("������ƮǮ���� ����ؾ� �ϴ� ����");

        //ĳ���� ������ ��ȯ
        //����ü ����
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();

        projective.transform.SetParent(player.SelectCharacter.transform);
        projective.transform.localPosition = new Vector3(0, 0.5f, 0);  
        projective.Attributes.Add(new P_Rotation(projective, 1.2f, 0.1f, 2.2f, 1.9162f, 1));
        projective.Attributes.Add(new P_Damage(this, damage));
        projectives.Add(projective);
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

    }
}
