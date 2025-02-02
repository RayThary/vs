using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

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

    public int MaxLevel => 5;

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
        level++;
        if (level >= 5)
        {

        }
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
            GameManager.Instance.StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        timer = Time.time;
        for (int i = 0; i < player.Stat.AttackCount + level; i++)
        {
            //방향을 설정해야 함
            //상대 방향
            Vector2 dir = GameManager.Instance.GetTargetTrs.position - player.SelectCharacter.transform.position;
            //각도에 랜덤성이 있어야 함
            float angle = Vector2.Angle(Vector2.up, dir) + Random.Range(-10, 11);
            if (GameManager.Instance.GetTargetTrs.position.x < player.SelectCharacter.transform.position.x)
            {
                angle = -angle;
            }
            //각도를 vector로
            dir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

            //투사체 설정
            Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Arrow, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
            projective.Init();
            projective.transform.position = player.SelectCharacter.transform.position;
            projective.transform.eulerAngles = new Vector3(0, 0, -angle);
            //움직임
            projective.Attributes.Add(new P_Move(projective, dir, 5));
            //도착하면 터지도록
            projective.Attributes.Add(new P_DeleteTimer(projective, 10));
            projective.Attributes.Add(new P_Damage(this, damage));

            projectives.Add(projective);
            timer = Time.time;
            yield return new WaitForSeconds(0.1f);
        }
        timer = Time.time;
    }
}
