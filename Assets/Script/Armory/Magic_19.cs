using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magic_19 : IAddon
{
    public string AddonName => "10";

    public Sprite Sprite => GameManager.Instance.Magic[18];

    private readonly string description;
    public string Description => description;

    public bool Weapon => true;

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    private readonly Player player;
    //발사할 발사체 원본
    private readonly Projective projective;

    private readonly List<Projective> projectives = new();

    //상대 이동속도 저하
    private readonly float speed;
    //대미지
    private readonly float damage;

    private readonly float cycle;

    public Magic_19(Player player)
    {
        this.player = player;
        projective = Resources.Load<Projective>("Magic/Magic_19");
        description = "플레이어 주위에 피해를 입히며 상대를 느려지게 한다";
        this.player = player;
        speed = 0.3f;
        damage = 2;
        cycle = 1;
        level = 0;
    }

    public void Update()
    {
        Fire();
        level = 1;
    }

    public void Addon()
    {

    }

    public void Remove()
    {
        level = 0;
        //모든 발사체 삭제
        Debug.Log("오브젝트 풀링을 사용하지 않는 삭제");
        projectives.ForEach(x => Object.Destroy(x.gameObject));
    }

    public void LevelUp()
    {
        //강화는 레벨없이 없어
    }

    private void Fire()
    {
        //투사체 설정
        Debug.Log("오브젝트 풀링을 사용하지 않는 생성");
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();

        projective.transform.position = player.SelectCharacter.transform.position;
        projective.Attributes.Add(new P_DamageTimer(damage, cycle, this));
        projective.Attributes.Add(new P_Slow(speed));
        projectives.Add(projective);
    }
}
