using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//사거리 안의 적을 감지해서 가장 가까운 적에게 발사하는 장비
public class Magic_9 : IAddon
{
    private readonly Player player;
    //발사할 발사체 원본
    private readonly Projective projective;
    //사거리
    private readonly float range;
    //투사체 속도
    private readonly float speed;
    //대미지
    private readonly float damage;
    //공격 딜레이
    private readonly float delay;
    //공격 딜레마 계산 타이머
    private float timer;
    //스프라이트
    public Sprite Sprite {  get => GameManager.Instance.magic[8]; }
    //딜량
    private float statistics;
    public float Statistics { get { return statistics; } set { statistics = value; } }

    private List<Projective> projectives = new();

    public Magic_9(Player player, float range, float speed, float damage, float delay)
    {
        projective = Resources.Load<Projective>("Magic_9");
        this.player = player;
        this.range = range;
        this.speed = speed;
        this.damage = damage;
        this.delay = delay;
    }

    public void Addon()
    {
        timer = Time.time;
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
        //공격 딜레이가 되었는지
        if(timer + delay <= Time.time)
        {
            //적이 근처에 있는지
            Enemy enemy = DistanceMin(range);
            if (enemy != null)
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

                timer = Time.time;

                Debug.Log("오브젝트풀링을 사용해야 하는 생성");
                //투사체 설정
                Projective projective = Object.Instantiate(this.projective);
                projective.transform.position = player.transform.position + (Vector3)dir;
                projective.transform.eulerAngles = new Vector3(0, 0, -angle);
                projective.Attributes.Add(new P_Move(projective, dir, speed));
                projective.Attributes.Add(new P_Damage(this, damage));
                //projective.Attributes.Add(new P_EnterDelete(projective));
                projective.Attributes.Add(new P_DistanceDelete(7, projective.transform.position, projective));

                projectives.Add(projective);
            }
        }
    }

    //range안에 가장 가까운 적
    public Enemy DistanceMin(float range)
    {
        Enemy enemy = Enemy.enemyList
            .OrderBy(enemy => Vector3.Distance(player.transform.position, enemy.transform.position)) // 거리 기준으로 정렬
            .FirstOrDefault(); // 가장 가까운 적 반환 (리스트가 비어있으면 null 반환)
        if (enemy == null)
            return null;
        else if(Vector2.Distance(enemy.transform.position, player.transform.position) < range)
            return enemy;
        return null;
    }
}
