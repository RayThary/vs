using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Axe : IAddon
{
    public string AddonName => "Axe";

    private readonly Player player;
  
    private readonly List<Projective> projectives = new();

    private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Axe;

    private readonly string description;
    public string Description { get => description; }

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    //���� ������
    private readonly float delay;

    //���� ������ ��� Ÿ�̸�
    private float timer;
    public Magic_Axe(Player player)
    {
        
        description = "������ ������ ������";
        this.player = player;
        level = 0;
        damage = 2; 
        delay = 3;
    }

    public void Addon()
    {
        level = 1;
    }

    public void LevelUp()
    {

    }

    public void Remove()
    {
        level = 0;
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));
        projectives.Clear();
    }

    public void Update()
    {
        //���� �����̰� �Ǿ�����
        if (timer + (delay - player.Stat.AttackCool) <= Time.time)
        {
            GameManager.Instance.StartCoroutine(Fire());
            timer = Time.time;
        }
    }

    private IEnumerator Fire()
    {
        for (int i = 0; i < player.Stat.AttackCount + 1; i++)
        {
            //����ü ����
            Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Axe, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
            projective.Init();
            //���⼭ ������ �޾ƿ�
            //ĳ���Ͱ� �ٶ󺸴� ����
            //�������� ���� �������� ��

            projective.transform.position = player.SelectCharacter.transform.position;
            projective.Attributes.Add(new P_Move(projective, Vector2.up, 15));
            if (GameManager.Instance.GetTargetTrs.position.x > player.SelectCharacter.transform.position.x)
            {
                projective.Attributes.Add(new P_Move(projective, Vector2.left, 5));
            }
            else
            {
                projective.Attributes.Add(new P_Move(projective, Vector2.right, 5));
            }
            projective.Attributes.Add(new P_Rotation(projective, 180));
            projective.Attributes.Add(new P_Gravity(projective));
            projective.Attributes.Add(new P_Damage(this, damage));
            projective.Attributes.Add(new P_DeleteTimer(projective, 10));
            projectives.Add(projective);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
