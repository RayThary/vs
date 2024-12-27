using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_8 : IAddon
{
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

    private List<Projective> projectives = new();

    public Magic_8(Player player, float speed, float damage)
    {
        projective = Resources.Load<Projective>("Magic_8");
        this.player = player;
        this.speed = speed;
        this.damage = damage;
    }

    public void Addon()
    {
        //랜덤한 위치
        Debug.Log("오브젝트풀링을 사용해야 하는 생성");

        for(int i = 0; i < 3; i++)
        {
            //투사체 설정
            Projective projective = Object.Instantiate(this.projective);
            projective.Init();
            //여기서 방향을 받아옴
            Vector2 dir = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f));

            projective.transform.position = player.transform.position + (Vector3)dir;
            //projective.transform.eulerAngles = new Vector3(0, 0, -angle);
            projective.Attributes.Add(new P_Move(projective, dir, speed));
            projective.Attributes.Add(new P_Bounce(projective, projective.Attributes.OfType<P_Move>().FirstOrDefault(), 1));
            projective.Attributes.Add(new P_Damage(this, damage));
            projectives.Add(projective);
        }
    }

    public void LevelUp()
    {
        //투사체 설정
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();
        //여기서 방향을 받아옴
        Vector2 dir = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f));

        projective.transform.position = player.transform.position + (Vector3)dir;
        //projective.transform.eulerAngles = new Vector3(0, 0, -angle);
        projective.Attributes.Add(new P_Move(projective, dir, speed));
        projective.Attributes.Add(new P_Bounce(projective, projective.Attributes.OfType<P_Move>().FirstOrDefault(), 1));
        projective.Attributes.Add(new P_Damage(this, damage));
        projectives.Add(projective);
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

    }
}
