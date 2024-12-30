using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_6 : IAddon
{
    private readonly Player player;
    //발사할 발사체 원본
    private readonly Projective projective;
    //대미지
    private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Magic[5];

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public bool Weapon => true;

    private List<Projective> projectives = new();

    public Magic_6(Player player, float damage)
    {
        projective = Resources.Load<Projective>("Magic/Magic_6");
        this.player = player;
        this.damage = damage;
    }

    public void Addon()
    {
        Debug.Log("오브젝트풀링을 사용해야 하는 생성");

        //캐릭터 하위에 소환
        //투사체 설정
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();

        projective.transform.SetParent(player.SelectCharacter.transform);
        projective.transform.localPosition = new Vector3(0, 0.5f, 0);  
        projective.Attributes.Add(new P_Rotation(projective, 1.2f, 0.1f, 2.2f, 1.9162f, 1));
        projective.Attributes.Add(new P_Damage(this, damage));
        projectives.Add(projective);
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
