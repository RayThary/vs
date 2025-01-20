using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Axe : IAddon
{
    public string AddonName => "Axe";

    private readonly Player player;
    //발사할 발사체 원본
    private readonly Projective projective;
    private readonly List<Projective> projectives = new();

    private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Axe;

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
    public Magic_Axe(Player player)
    {
        projective = Resources.Load<Projective>("Magic/Axe");
        description = "앞으로 도끼를 던진다";
        this.player = player;
        level = 0;
        damage = 2; 
        delay = 3;
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
        Debug.Log("오브젝트 풀링을 사용하지 않는 삭제");
        projectives.ForEach(x => Object.Destroy(x.gameObject));
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
        Debug.Log("오브젝트 풀링을 사용하지 않는 생성");
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();
        //여기서 방향을 받아옴
        //캐릭터가 바라보는 방향
        //움직임이 점점 느려져야 함

        projective.transform.position = player.SelectCharacter.transform.position;
        projective.Attributes.Add(new P_Move(projective, Vector2.up, 15));
        if(player.SelectCharacter.transform.localScale.x > 0)
        {
            projective.Attributes.Add(new P_Move(projective, Vector2.left, 5));
        }
        else
        {
            projective.Attributes.Add(new P_Move(projective, Vector2.right, 5));
        }
        projective.Attributes.Add(new P_Rotation(projective, 180));
        projective.Attributes.Add(new P_Gravity(projective));
        projective.Attributes.Add(new P_Damage(this, damage)); 
        projective.Attributes.Add(new P_DeleteTimer(projective, 10));
        projectives.Add(projective);
    }
}
