using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_7 : IAddon
{
    public string AddonName => "7";

    private readonly Player player;
    //발사할 발사체 원본
    private readonly Projective projective;
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
        projective = Resources.Load<Projective>("Magic/Magic_7");
        description = "플레이어의 주변에 회전하는 불꽃을 생성한다";
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

    }

    public void Remove()
    {
        //모든 발사체 삭제
        Debug.Log("오브젝트 풀링을 사용하지 않는 삭제");
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
    }

    private void Fire(int angle)
    {
        //0 0 0 0, 90 -1 0 0, 180 0 -0.9 0, 270 1 0 0
        Vector3 position;
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
        Debug.Log("오브젝트 풀링을 사용하지 않는 생성");

        //캐릭터 하위에 소환
        //투사체 설정
        Projective projective = Object.Instantiate(this.projective);
        Animator n = projective.transform.GetChild(0).GetComponent<Animator>();
        projective.Init();

        projective.transform.SetParent(player.SelectCharacter.transform);
        projective.transform.localPosition = position;
        projective.transform.localEulerAngles = new Vector3(0, 0, angle);
        projective.Attributes.Add(new P_Damage(this, damage));
        projectives.Add(projective);


        if (animator != null)
        {
            float referenceTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            n.Play("Magic_7", -1, referenceTime % 1);
        }
    }
}
