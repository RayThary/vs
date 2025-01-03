using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_11 : IAddon
{
    //9번 진화
    public string AddonName => "1";

    public Sprite Sprite => GameManager.Instance.Magic[10];

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

    //투사체 속도
    private readonly float speed;
    //대미지
    private readonly float damage;
    //공격 딜레이
    private readonly float delay;

    //공격 딜레마 계산 타이머
    private float timer;

    public Magic_11(Player player)
    {
        projective = Resources.Load<Projective>("Magic/Magic_9");
        description = "불꽃을 가장 가까운 적에게 발사한다";
        this.player = player;
        speed = 5;
        damage = 4;
        delay = 1;
        level = 0;
    }

    public void Addon()
    {
        timer = Time.time;
        level = 1;
    }

    public void LevelUp()
    {
        //강화는 레벨없이 없음
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
        //적이 근처에 있는지
        if(GameManager.Instance.GetTargetTrs.TryGetComponent(out Enemy enemy))
        {
            //방향을 설정해야 함
            //상대 방향
            Vector2 dir = enemy.transform.position - player.transform.position;
            //각도
            float angle = Vector2.Angle(Vector2.up, dir);
            if (enemy.transform.position.x < player.transform.position.x)
            {
                angle = -angle;
            }
            //각도를 vector로
            dir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

            Debug.Log("오브젝트풀링을 사용해야 하는 생성");
            //투사체 설정
            Projective projective = Object.Instantiate(this.projective);
            projective.transform.position = player.transform.position + (Vector3)dir;
            projective.transform.eulerAngles = new Vector3(0, 0, -angle);
            projective.Attributes.Add(new P_Move(projective, dir, speed));
            projective.Attributes.Add(new P_Damage(this, damage));
            projective.Attributes.Add(new P_DistanceDelete(7, projective.transform.position, projective));

            projectives.Add(projective);
        }
    }
}
