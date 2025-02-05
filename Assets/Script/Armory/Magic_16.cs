using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_16 : IAddon
{
    public string AddonName => "16";

    private readonly Player player;
    
    //대미지
    private float damage;
    //공격 딜레이
    private float delay;
    //공격 딜레마 계산 타이머
    private float timer;
    //스프라이트
    public Sprite Sprite { get => GameManager.Instance.Magic[15]; }

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

    public Magic_16(Player player)
    {
        description = "폭발로 적을 공격한다";
        this.player = player;
        damage = 1;
        delay = 5;
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
        damage += 1;
        delay -= 0.2f;
        if (level == MaxLevel)
        {
            //서로 짝이되는 강화가 있어야 함 13번
            var power = player.Armory.Addons.OfType<AttackCool>().FirstOrDefault();
            if (power != null && power.Level == power.MaxLevel)
            {
                player.Armory.Remove(this);
                player.Armory.Addon(new Magic_13(player));
            }
        }
    }

    public void Remove()
    {
        damage = 1;
        delay = 5;
        level = 0;
        //모든 발사체 삭제
        
        projectives.ForEach(x => x.transform.localScale = new Vector3(1, 1, 1));
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));

        projectives.Clear();
    }

    public void Update()
    {
        //공격 딜레이가 되었는지
        if (timer + (delay - player.Stat.AttackCool) <= Time.time)
        {
            GameManager.Instance.StartCoroutine(Fire());
            timer = Time.time;
        }
    }

    private IEnumerator Fire()
    {
        for (int i = 0; i < player.Stat.AttackCount + 1; i++)
        {
            if (GameManager.Instance.GetTargetTrs == null)
                yield break;
            //발사 마우스 위치에 폭발
            //투사체 설정
            Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic15, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
            projective.Init();

            projective.transform.position = GameManager.Instance.GetTargetTrs.position;
            projective.Attributes.Add(new P_DamageTimer(damage, 1, this));
            projective.Attributes.Add(new P_DeleteTimer(projective, 3));
            projectives.Add(projective);
            yield return new WaitForSeconds(0.1f);
        }
            
    }
}
