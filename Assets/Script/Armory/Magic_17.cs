using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_17 : IAddon
{
    public string AddonName => "17";

    private readonly Player player;
    //투사체 속도
    private readonly float speed;
    //대미지
    private readonly float damage;
    //스프라이트
    public Sprite Sprite { get => GameManager.Instance.Magic[16]; }

    private readonly string description;
    public string Description { get => description; }

    public bool Weapon => true;

    //딜량
    private float statistics;
    public float Statistics { get { return statistics; } set { statistics = value; } }

    private readonly List<Projective> projectives = new();

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    public Magic_17(Player player)
    {
        description = "벽에 튕기는 큰 구체를 하나를 발사한다";
        this.player = player;
        speed = 3;
        damage = 20;
        level = 0;
    }

    public void Addon()
    {
        Fire();
        level = 1;
    }

    public void LevelUp()
    {
        //강화는 레벨없이 없어
    }

    public void Remove()
    {
        level = 0;
        //모든 발사체 삭제
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));
        projectives.ForEach(x => x.transform.localScale = new Vector3(1, 1, 1));

        projectives.Clear();
    }

    public void Update()
    {
        if (projectives.Count - level != player.Stat.AttackCount)
        {
            for (int i = 0; i < player.Stat.AttackCount - (projectives.Count - level); i++)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        //발사 마우스 위치나 가장 가까운 적
        //투사체 설정
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic6, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
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
