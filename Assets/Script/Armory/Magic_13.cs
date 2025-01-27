using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_13 : IAddon
{
    public string AddonName => "13";

    private readonly Player player;
    //대미지
    private readonly float damage;
    //공격 딜레이
    private readonly float delay;
    //공격 딜레마 계산 타이머
    private float timer;
    //스프라이트
    public Sprite Sprite { get => GameManager.Instance.Magic[12]; }

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

    public Magic_13(Player player)
    {
        description = "폭발로 적을 공격한다";
        this.player = player;
        damage = 3;
        delay = 3;
        level = 0;
    }

    public void Addon()
    {
        timer = Time.time;
        level = 1;
    }

    public void LevelUp()
    {
        //강화는 레벨업이 없음
    }

    public void Remove()
    {
        level = 0;
        //모든 발사체 삭제
        projectives.ForEach(x => x.transform.localScale = new Vector3(1, 1, 1));
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));

        projectives.Clear();
    }

    public void Update()
    {
        //공격 딜레이가 되었는지
        if (timer + (delay - player.Stat.AttackCool) <= Time.time)
        {
            for (int i = 0; i < player.Stat.AttackCount + 1; i++)
            {
                Fire();
            }
            timer = Time.time;
        }
    }

    private void Fire()
    {
        if (GameManager.Instance.GetTargetTrs == null)
            return;

        //발사 마우스 위치에 폭발
        //투사체 설정
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic13, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
        projective.Init();

        projective.transform.position = GameManager.Instance.GetTargetTrs.position;
        projective.Attributes.Add(new P_DamageTimer(damage, 1, this));
        projective.Attributes.Add(new P_DeleteTimer(projective, 3));
        projectives.Add(projective);
    }
}
