using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_8 : IAddon
{
    public string AddonName => "8";

    private readonly Player player;
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

    private bool enhance = false;
    public bool Enhance { get { return enhance; } set { enhance = value; } }

    public Magic_8(Player player)
    {
        description = "벽에 튕기는 구체를 3발 발사한다";
        this.player = player;
        speed = 5;
        damage = 5;
        level = 0;
    }

    public void Addon()
    {
        //랜덤한 위치
        for(int i = 0; i < 3; i++)
        {
            Fire();
        }
        level = 1;
    }

    //하나 더 발사
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
        level = 0;
        //모든 발사체 삭제
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));
        projectives.Clear();
        enhance = false;
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
        if (level == MaxLevel)
        {
            //8 + 스피드 = +1
            var power = player.Armory.Addons.OfType<Speed>().FirstOrDefault();
            if (power != null && power.Level == power.MaxLevel)
            {
                if (!enhance)
                    enhance = true;
            }
        }
    }

    private void Fire()
    {
        //투사체 설정
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic8, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
        projective.Init();
        //여기서 방향을 받아옴
        Vector2 dir = new(Random.Range(-1, 1f), Random.Range(-1, 1f));

        projective.transform.position = player.SelectCharacter.transform.position + (Vector3)dir;
        projective.Attributes.Add(new P_Move(projective, dir, speed));
        projective.Attributes.Add(new P_Bounce(projective, projective.Attributes.OfType<P_Move>().FirstOrDefault(), 1));
        projective.Attributes.Add(new P_Damage(this, damage)); 
        if (enhance)
        {
            projective.Attributes.Add(new P_KillCreate(PoolingManager.ePoolingObject.Magic5, this, 3));
        }
        projectives.Add(projective);
    }
}
