using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Boom : IAddon
{
    public string AddonName => "Boom";

    private readonly Player player;
    private readonly List<Projective> projectives = new();

    private readonly float damage;

    public Sprite Sprite => GameManager.Instance.Magic[2];

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
    public Magic_Boom(Player player)
    {
        description = "앞으로 폭탄을 던진다";
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
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject)); 
        projectives.Clear();
    }

    public void Update()
    {
        //공격 딜레이가 되었는지
        if (timer + delay <= Time.time)
        {
            GameManager.Instance.StartCoroutine(Fire());
            timer = Time.time;
        }
    }

    private IEnumerator Fire()
    {
        for (int i = 0; i < player.Stat.AttackCount + 1; i++)
        {
            //투사체 설정
            Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic3, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
            projective.Init();

            projective.transform.position = player.SelectCharacter.transform.position;
            //타겟팅 방향으로 원운동  //도착하면 삭제 소환
            if (GameManager.Instance.GetTargetTrs != null)
                projective.Attributes.Add(new P_CircularDestroy(projective, PoolingManager.ePoolingObject.Magic20, GameManager.Instance.GetTargetTrs.position, 90, 5));
            else
                projective.Attributes.Add(new P_CircularDestroy(projective, PoolingManager.ePoolingObject.Magic20, player.SelectCharacter.transform.position + new Vector3(3, 0, 0), 90, 5));
            projective.Attributes.Add(new P_Damage(this, damage));

            projectives.Add(projective);
            yield return new WaitForSeconds(0.1f);
        } 
    }
}
