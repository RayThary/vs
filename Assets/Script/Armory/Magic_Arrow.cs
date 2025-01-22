using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Arrow : IAddon
{
    public string AddonName => "Arrow";

    private readonly Player player;
    private readonly List<Projective> projectives = new();

    private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Arrow;

    private readonly string description;
    public string Description { get => description; }

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    //공격 딜레이
    private readonly float delay;

    //공격 딜레마 계산 타이머
    private float timer;

    public Magic_Arrow(Player player)
    {
        description = "앞으로 화살을 발사한다";
        this.player = player;
        level = 0;
        damage = 1;
        delay = 1;
    }

    public void Addon()
    {
        level = 1;
    }

    public void LevelUp()
    {

    }

    public void Remove()
    {
        level = 0;
        //모든 발사체 삭제
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));
        projectives.Clear();
    }

    public void Update()
    {
        //공격 딜레이가 되었는지
        if (timer + delay <= Time.time)
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
        //투사체 설정
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Arrow, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
        projective.Init();

        projective.transform.position = player.SelectCharacter.transform.position;
        //움직임
        projective.Attributes.Add(new P_Move(projective, GameManager.Instance.GetTargetTrs.position, 5));
        //도착하면 터지도록
        projective.Attributes.Add(new P_DeleteTimer(projective, 10));
        projective.Attributes.Add(new P_Damage(this, damage));

        projectives.Add(projective);
    }
}
