using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_15 : IAddon
{
    private readonly Player player;
    //발사할 발사체 원본
    private readonly Projective projective;
    //투사체 속도
    private readonly float speed;
    //대미지
    private readonly float damage;
    //스프라이트
    public Sprite Sprite { get => GameManager.Instance.Magic[14]; }
    public bool Weapon => true;

    //딜량
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
        //발사 마우스 위치나 가장 가까운 적
        Debug.Log("오브젝트풀링을 사용해야 하는 생성");
        //투사체 설정
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();
        //여기서 방향을 받아옴
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
        //크기 커지게
        projectives.ForEach(x => x.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f));
        projectives.ForEach(x => x.Attributes.OfType<P_Bounce>().FirstOrDefault().Size += 0.5f);
    }

    public void Remove()
    {
        //모든 발사체 삭제
        Debug.Log("오브젝트 풀링을 사용하지 않는 삭제");
        projectives.ForEach(x => x.transform.localScale = new Vector3(1, 1, 1));
        projectives.ForEach(x => Object.Destroy(x.gameObject));

        projectives.Clear();
    }

    public void Update()
    {
        //한번만 공격하는 거니까
    }
}
