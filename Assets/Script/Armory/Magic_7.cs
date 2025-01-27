using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_7 : IAddon
{
    public string AddonName => "7";

    private readonly Player player;
    //�߻�� �༮��
    private readonly List<Projective> projectives = new();

    private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Magic[6];

    private readonly string description;
    public string Description { get => description; }

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    public Magic_7(Player player)
    {
        description = "�÷��̾��� �ֺ��� ȸ���ϴ� �Ҳ��� �����Ѵ�";
        this.player = player;
        level = 0;
        damage = 4;
    }

    public void Addon()
    {
        Fire(0);
        
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
        //�⺻������ �־����� �ϳ��� ���� ��
        if (projectives.Count - 1 < player.Stat.AttackCount)
        {
            Fire(90 * (player.Stat.AttackCount + (projectives.Count - player.Stat.AttackCount)));
        }
        if (projectives.Count > 0 && projectives[0].transform.GetChild(0).TryGetComponent(out Animator component))
        {
            if (component.speed != player.Stat.AttackSpeed)
                component.speed = player.Stat.AttackSpeed;
        }
    }

    private void Fire(int angle)
    {
        //0 0 0 0, 90 -1 0 0, 180 0 -0.9 0, 270 1 0 0
        //��ġ
        Vector3 position;
        //������ ����ü�� �ִϸ��̼�
        Animator animator;
        switch (angle)
        {
            case 0:
                position = new Vector3(0, 1.2f, 0);
                animator = null;
                break;
            case 90:
                position = new Vector3(-1, 0, 0);
                animator = projectives[0].transform.GetChild(0).GetComponent<Animator>();
                break;
            case 180:
                position = new Vector3(0, -0.9f, 0);
                animator = projectives[0].transform.GetChild(0).GetComponent<Animator>();
                break;
            case 270:
                position = new Vector3(1, 0, 0);
                animator = projectives[0].transform.GetChild(0).GetComponent<Animator>();
                break;
            default:
                return;
                //position = new Vector3(0, 0, 0);
                //animator = projectives[0].transform.GetChild(0).GetComponent<Animator>();
                //break;
        }
        //����ü ����
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic7, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
        //�� ������Ʈ�� �ִϸ��̼�
        Animator n = projective.transform.GetChild(0).GetComponent<Animator>();
        projective.Init();

        projective.transform.position = position;
        projective.transform.localEulerAngles = new Vector3(0, 0, angle);
        projective.Attributes.Add(new P_Follow(projective, position, player.SelectCharacter.transform));
        projective.Attributes.Add(new P_Damage(this, damage));
        projectives.Add(projective);


        if (animator != null)
        {
            float referenceTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            n.Play("Magic_7", -1, referenceTime % 1);
        }
    }
}
