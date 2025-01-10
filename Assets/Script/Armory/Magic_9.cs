using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//사거리 안의 적을 감지해서 가장 가까운 적에게 발사하는 장비
public class Magic_9 : IAddon
{
    public string AddonName => "9";

    private readonly Player player;
    //발사할 발사체 원본
    private readonly Projective projective;
    //투사체 속도
    private readonly float speed;
    //대미지
    private float damage;
    //공격 딜레이
    private readonly float delay;
    //공격 딜레마 계산 타이머
    private float timer;
    //스프라이트
    public Sprite Sprite { get => GameManager.Instance.Magic[8]; }

    private readonly string description;
    public string Description { get => description; }

    public bool Weapon => true;

    //딜량
    private float statistics;
    public float Statistics { get { return statistics; } set { statistics = value; } }

    private readonly List<Projective> projectives = new();

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    public Magic_9(Player player)
    {
        projective = Resources.Load<Projective>("Magic/Magic_9");
        description = "불꽃을 가장 가까운 적에게 발사한다";
        this.player = player;
        speed = 5;
        damage = 1;
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
        level++;
        damage += 0.5f;
        if (level == MaxLevel)
        {
            //서로 짝이되는 강화가 있어야 함 11번이 강호된 마법
            var power = player.Armory.Addons.OfType<AttackSpeed>().FirstOrDefault();
            if (power != null && power.Level == power.MaxLevel)
            {
                player.Armory.Remove(this);
                player.Armory.Addon(new Magic_11(player));
            }
        }
    }

    public void Remove()
    {
        //모든 발사체 삭제
        Debug.Log("오브젝트 풀링을 사용하지 않는 삭제");
        damage = 1;
        projectives.ForEach(x => Object.Destroy(x.gameObject));
        projectives.Clear();
    }

    public void Update()
    {
        //공격 딜레이가 되었는지
        if(timer + (delay - player.Stat.AttackCool)  <= Time.time)
        {
            for(int i = 0; i < player.Stat.AttackCount + 1; i++)
            {
                Fire();
            }
            timer = Time.time;
        }
    }

    private void Fire()
    {
        //적이 근처에 있는지
        if (GameManager.Instance.GetTargetTrs.TryGetComponent(out Enemy enemy))
        {
            //방향을 설정해야 함
            //상대 방향
            Vector2 dir = enemy.transform.position - player.SelectCharacter.transform.position;
            //각도에 랜덤성이 있어야 함
            float angle = Vector2.Angle(Vector2.up, dir) + Random.Range(-10, 11);
            if (enemy.transform.position.x < player.SelectCharacter.transform.position.x)
            {
                angle = -angle;
            }
            //각도를 vector로
            dir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

            Debug.Log("오브젝트 풀링을 사용하지 않는 생성");
            //투사체 설정
            Projective projective = Object.Instantiate(this.projective);
            projective.Init();
            projective.transform.position = player.SelectCharacter.transform.position + (Vector3)dir;
            projective.transform.eulerAngles = new Vector3(0, 0, -angle);
            projective.Attributes.Add(new P_Move(projective, dir, speed));
            projective.Attributes.Add(new P_Damage(this, damage));
            //projective.Attributes.Add(new P_EnterDelete(projective));
            projective.Attributes.Add(new P_DistanceDelete(7, projective.transform.position, projective));

            projectives.Add(projective);
        }
    }
}
