using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//18번 강화로 magic5 자체는 써도 스크립트는 안쓰는중
public class Magic_5 : IAddon
{
    public string AddonName => "5";

    //private readonly Player player;
    //대미지
    //private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Magic[4];

    private readonly string description;
    public string Description { get => description; }

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private readonly List<Projective> projectives = new();

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    //public Magic_5(Player player)
    //{
    //    description = "자신과 닿은 적에게 지속적으로 피해를 입힌다.";
    //    this.player = player;
    //    damage = 1;
    //    level = 0;
    //}

    public void Addon()
    {
        level = 1;
    }

    public void LevelUp()
    {
        //강화는 레벨업이 없어
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

    }

    //private void Fire(int angle)
    //{
        
    //}
}
