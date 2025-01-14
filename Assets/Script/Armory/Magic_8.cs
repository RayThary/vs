using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_8 : IAddon
{
    public string AddonName => "8";

    private readonly Player player;
    //발사할 발사체 원본
    private readonly Projective projective;
    //투사체 속도
    private readonly float speed;
    //대미지
    private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Magic[7];

    private readonly string description;
    public string Description { get => description; }

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private readonly List<Projective> projectives = new();

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public Magic_8(Player player)
    {
        projective = Resources.Load<Projective>("Magic/Magic_8");
        description = "벽에 튕기는 구체를 3발 발사한다";
        this.player = player;
        speed = 5;
        damage = 1;
        level = 0;
    }

    public void Addon()
    {
        //랜덤한 위치
        Debug.Log("오브젝트풀링을 사용해야 하는 생성");

        for(int i = 0; i < 3; i++)
        {
            Fire();
        }
        level = 1;
    }

    public void LevelUp()
    {
        Fire(); 
        level++;
        if (level == MaxLevel)
        {
            //서로 짝이되는 강화가 있어야 함
        }
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
        //기본 3발 + 레벨 1 == 기본 0 이되려면 2를 더해야 함
        if(projectives.Count - 2 - level != player.Stat.AttackCount)
        {
            for (int i = 0; i < player.Stat.AttackCount - (projectives.Count - 3 - level); i++)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        //투사체 설정
        Debug.Log("오브젝트 풀링을 사용하지 않는 생성");
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();
        //여기서 방향을 받아옴
        Vector2 dir = new(Random.Range(-1, 1f), Random.Range(-1, 1f));

        projective.transform.position = player.SelectCharacter.transform.position + (Vector3)dir;
        projective.Attributes.Add(new P_Move(projective, dir, speed));
        projective.Attributes.Add(new P_Bounce(projective, projective.Attributes.OfType<P_Move>().FirstOrDefault(), 1));
        projective.Attributes.Add(new P_Damage(this, damage));
        projectives.Add(projective);
    }
}
