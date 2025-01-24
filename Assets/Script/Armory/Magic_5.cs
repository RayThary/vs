using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_5 : IAddon
{
    public string AddonName => "5";

    private readonly Player player;
    //�����
    private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Magic[4];

    private readonly string description;
    public string Description { get => description; }

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private readonly List<Projective> projectives = new();

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    public Magic_5(Player player)
    {
        description = "�ڽŰ� ���� ������ ���������� ���ظ� ������.";
        this.player = player;
        damage = 1;
        level = 0;
    }

    public void Addon()
    {
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

    }

    private void Fire(int angle)
    {
        //0 0 0 0, 90 0.4 0.4 0, 180 0 0.7 0, 270 -0.4 0.5 0
        //��ġ
        Vector3 position;
        //������ ����ü�� �ִϸ��̼�
        Animator animator;
        switch (angle)
        {
            case 0:
                position = new Vector3(0, 0, 0);
                animator = null;
                break;
            case 90:
                position = new Vector3(0.4f, 0.4f, 0);
                animator = projectives[0].transform.GetChild(0).GetComponent<Animator>();
                break;
            case 180:
                position = new Vector3(0, 0.7f, 0);
                animator = projectives[0].transform.GetChild(0).GetComponent<Animator>();
                break;
            case 270:
                position = new Vector3(-0.4f, 0.5f, 0);
                animator = projectives[0].transform.GetChild(0).GetComponent<Animator>();
                break;
            default:
                return;
        }
        //����ü ����
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic_6, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
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
            n.Play("Magic_6", -1, referenceTime % 1);
        }
    }
}
