using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_6 : IAddon
{
    public string AddonName => "6";

    private readonly Player player;
    //발사할 발사체 원본
    private readonly Projective projective;
    //대미지
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
        description = "플레이어의 주변에 일정 주기마다 회전하는 불꽃을 생성한다";
        this.player = player;
        damage = 1;
        level = 0;
    }

    public void Addon()
    {
        Fire(0);
        level = 1;
    }

    //크기 증가
    public void LevelUp()
    {
        level++;
        projectives.ForEach(x => x.transform.localScale += new Vector3(0.5f, 0.5f, 0));
        if(level == MaxLevel)
        {
            //서로 짝이되는 강화가 있어야 함 7번이 강화된 마법
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
        //모든 발사체 삭제
        Debug.Log("오브젝트 풀링을 사용하지 않는 삭제");
        projectives.ForEach(x => x.transform.localScale = new Vector3(1, 1, 1));
        projectives.ForEach(x => Object.Destroy(x.gameObject));
        projectives.Clear();
    }

    public void Update()
    {
        //기본적으로 주어지는 하나는 빼야 함
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
        //위치
        Vector3 position;
        //최초의 투사체의 애니메이션
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
        Debug.Log("오브젝트 풀링을 사용하지 않는 생성");

        //투사체 설정
        Projective projective = Object.Instantiate(this.projective);
        //새 오브젝트의 애니메이션
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
