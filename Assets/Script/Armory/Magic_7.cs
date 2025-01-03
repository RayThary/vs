using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_7 : IAddon
{
    public string AddonName => "7";

    private readonly Player player;
    //발사할 발사체 원본
    private readonly Projective projective;
    private readonly List<Projective> projectives = new();

    public Sprite Sprite => GameManager.Instance.Magic[6];

    private readonly string description;
    public string Description { get => description; }

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 1;

    public Magic_7(Player player)
    {
        projective = Resources.Load<Projective>("Magic/Magic_7");
        description = "플레이어의 주변에 회전하는 불꽃을 생성한다";
        this.player = player;
        level = 0;
    }

    public void Addon()
    {
        Debug.Log("오브젝트 풀링을 사용하지 않는 생성");

        //캐릭터 하위에 소환
        //투사체 설정
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();

        projective.transform.SetParent(player.SelectCharacter.transform);
        projective.transform.localPosition = new Vector3(0, 0.5f, 0);
        projective.Attributes.Add(new P_Rotation(projective, 0.7875f, 0.15f, 2f, 0f, 1.8f));
        projective.Attributes.Add(new P_Damage(this, 1));
        projectives.Add(projective);
        level = 1;
    }

    public void LevelUp()
    {

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
