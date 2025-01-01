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

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private readonly List<Projective> projectives = new();

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public Magic_8(Player player, float speed, float damage)
    {
        projective = Resources.Load<Projective>("Magic/Magic_8");
        this.player = player;
        this.speed = speed;
        this.damage = damage;
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
    }

    public void LevelUp()
    {
        Fire();
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
        if(projectives.Count - 3 - level != player.Stat.AttackCount)
        {
            Debug.Log("차이 발생");
            Debug.Log(player.Stat.AttackCount - (projectives.Count - 3 - level));
            Debug.Log(player.Stat.AttackCount);
            Debug.Log(projectives.Count - 3 - level);

            for (int i = 0; i < player.Stat.AttackCount - (projectives.Count - 3 - level); i++)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        //투사체 설정
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();
        //여기서 방향을 받아옴
        Vector2 dir = new(Random.Range(-1, 1f), Random.Range(-1, 1f));

        projective.transform.position = player.transform.position + (Vector3)dir;
        //projective.transform.eulerAngles = new Vector3(0, 0, -angle);
        projective.Attributes.Add(new P_Move(projective, dir, speed));
        projective.Attributes.Add(new P_Bounce(projective, projective.Attributes.OfType<P_Move>().FirstOrDefault(), 1));
        projective.Attributes.Add(new P_Damage(this, damage));
        projectives.Add(projective);
    }
}
