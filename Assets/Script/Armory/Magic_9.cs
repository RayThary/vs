using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//사거리 안의 적을 감지해서 가장 가까운 적에게 발사하는 장비
public class Magic_9 : IAddon
{
    public string AddonName => "9";

    private readonly Player player;
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
        description = "불꽃을 가장 가까운 적에게 발사한다";
        this.player = player;
        speed = 5;
        damage = 5;
        delay = 1;
        level = 0;
    }
    public void Addon()
    {
        timer = Time.time;
        level = 1;
    }

    //대미지 증가
    public void LevelUp()
    {
        level++;
        damage += 1f;
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
        level = 0;
        damage = 1;
        //모든 발사체 삭제
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));
        projectives.Clear();
        if (coroutine != null)
            GameManager.Instance.StopCoroutine(coroutine);
        coroutine = null;
    }

    private Coroutine coroutine;
    public void Update()
    {
        if ((player.Stat.AttackCount + level * 0.1f) >= timer + (delay - player.Stat.AttackCool))
        {
            if (coroutine == null)
                coroutine = GameManager.Instance.StartCoroutine(ContinuousFire());
        }
        else
        {
            //공격 딜레이가 되었는지
            if (timer + (delay - player.Stat.AttackCool) <= Time.time)
            {
                GameManager.Instance.StartCoroutine(Fire());
            }
        }
    }

    private IEnumerator Fire()
    {
        timer = Time.time;
        for (int i = 0; i < player.Stat.AttackCount + 1; i++)
        {
            //방향을 설정해야 함
            //상대 방향
            Vector2 dir = GameManager.Instance.GetTargetTrs.position - player.SelectCharacter.transform.position;
            //각도 랜덤성이 있어야 함
            float angle = Vector2.Angle(Vector2.up, dir);
            //첫발은 근데 랜덤성 없어야 할듯
            if (i != 0)
                angle += Random.Range(-10, 11);

            if (GameManager.Instance.GetTargetTrs.position.x < player.SelectCharacter.transform.position.x)
            {
                angle = -angle;
            }
            //각도를 vector로
            dir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

            //투사체 설정
            Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic9, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
            projective.Init();
            projective.transform.position = player.SelectCharacter.transform.position + (Vector3)dir;
            projective.transform.eulerAngles = new Vector3(0, 0, -angle);
            projective.Attributes.Add(new P_Move(projective, dir, speed));
            projective.Attributes.Add(new P_Damage(this, damage));
            projective.Attributes.Add(new P_DistanceDelete(7, projective.transform.position, projective));

            projectives.Add(projective);

            yield return new WaitForSeconds(0.1f);
        }
        timer = Time.time;
    }

    private IEnumerator ContinuousFire()
    {
        while (true)
        {
            //적이 근처에 있는지
            if (GameManager.Instance.GetTargetTrs.TryGetComponent(out Enemy enemy))
            {
                //방향을 설정해야 함
                //상대 방향
                Vector2 dir = enemy.transform.position - player.SelectCharacter.transform.position;
                //각도 랜덤성이 있어야 함
                float angle = Vector2.Angle(Vector2.up, dir);

                if (enemy.transform.position.x < player.SelectCharacter.transform.position.x)
                {
                    angle = -angle;
                }
                //각도를 vector로
                dir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

                //투사체 설정
                Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic9, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
                projective.Init();
                projective.transform.position = player.SelectCharacter.transform.position + (Vector3)dir;
                projective.transform.eulerAngles = new Vector3(0, 0, -angle);
                projective.Attributes.Add(new P_Move(projective, dir, speed));
                projective.Attributes.Add(new P_Damage(this, damage));
                projective.Attributes.Add(new P_DistanceDelete(7, projective.transform.position, projective));

                projectives.Add(projective);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
