using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_6 : IAddon
{
    public string AddonName => "6";

    private readonly Player player;
    //�߻��� �߻�ü ����
    private readonly Projective projective;
    //�����
    private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Magic[5];

    private readonly string description;
    public string Description { get => description; }

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private readonly List<Projective> projectives = new();

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public Magic_6(Player player)
    {
        projective = Resources.Load<Projective>("Magic/Magic_6");
        description = "�÷��̾��� �ֺ��� ���� �ֱ⸶�� ȸ���ϴ� �Ҳ��� �����Ѵ�";
        this.player = player;
        damage = 1;
        level = 0;
    }

    public void Addon()
    {
        Fire(0);
        level = 1;
    }

    //ũ�� ����
    public void LevelUp()
    {
        level++;
        projectives.ForEach(x => x.transform.localScale += new Vector3(0.5f, 0.5f, 0));
        if(level == MaxLevel)
        {
            //���� ¦�̵Ǵ� ��ȭ�� �־�� �� 7���� ��ȭ�� ����
            AttackDamage attack = player.Armory.Addons.OfType<AttackDamage>().FirstOrDefault();
            if (attack != null && attack.Level == attack.MaxLevel)
            {
                player.Armory.Remove(this);
                player.Armory.Addon(new Magic_7(player));
            }
        }
    }

    public void Remove()
    {
        level = 0;
        //��� �߻�ü ����
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");
        projectives.ForEach(x => x.transform.localScale = new Vector3(1, 1, 1));
        projectives.ForEach(x => Object.Destroy(x.gameObject));
        projectives.Clear();
    }

    public void Update()
    {
        //�⺻������ �־����� �ϳ��� ���� ��
        if (projectives.Count - 1 < player.Stat.AttackCount)
        {
            Fire(90 * (player.Stat.AttackCount + (projectives.Count - player.Stat.AttackCount)));
        }
        if(projectives.Count > 0 && projectives[0].transform.GetChild(0).TryGetComponent(out Animator component))
        {
            if(component.speed != player.Stat.AttackSpeed)
                component.speed = player.Stat.AttackSpeed;
        }
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
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");

        //����ü ����
        Projective projective = Object.Instantiate(this.projective);
        //�� ������Ʈ�� �ִϸ��̼�
        Animator n = projective.transform.GetChild(0).GetComponent<Animator>();
        projective.Init();

        //projective.transform.SetParent(player.SelectCharacter.transform);
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
