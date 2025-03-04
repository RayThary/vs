using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Magic_10 : IAddon
{
    public string AddonName => "10";

    public Sprite Sprite => GameManager.Instance.Magic[9];

    private readonly string description;
    public string Description => description;

    public bool Weapon => true;

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    private readonly Player player;

    private readonly List<Projective> projectives = new();

    //상대 이동속도 저하
    private float speed;
    //대미지
    private float damage;
    //공격 딜레이
    private readonly float delay;
    //공격 딜레마 계산 타이머
    private float timer;

    public Magic_10(Player player)
    {
        this.player = player;
        description = "플레이어 주위에 피해를 입히며 상대를 느려지게 한다";
        this.player = player;
        delay = 5;
        speed = 0.3f;
        damage = 5;
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
        speed += 0.1f;
        damage += 1;
        if (level == MaxLevel)
        {
            //서로 짝이되는 강화가 있어야 함 19
            var power = player.Armory.Addons.OfType<ShieldPoint>().FirstOrDefault();
            if (power != null && power.Level == power.MaxLevel)
            {
                player.Armory.Remove(this);
                player.Armory.Addon(new Magic_19(player));
            }
        }
    }

    public void Remove()
    {
        level = 0;
        speed = 0.3f;
        damage = 5;
        //모든 발사체 삭제
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));
        projectives.Clear();
    }

    public void Update()
    {
        //공격 딜레이가 되었는지
        if (timer + (delay - player.Stat.AttackCool) <= Time.time)
        {
            Fire();
        }

        if (projectives.Count > 0 && projectives[0].transform.GetChild(0).TryGetComponent(out Animator component))
        {
            if (component.speed != player.Stat.AttackSpeed)
                component.speed = player.Stat.AttackSpeed;
        }
    }

    private void Fire()
    {
        timer = Time.time;
        //투사체 설정
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic10, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
        projective.Init();

        projective.Attributes.Add(new P_Follow(projective, Vector2.up, player.SelectCharacter.transform));
        projective.Attributes.Add(new P_Damage(this, damage));
        projective.Attributes.Add(new P_SlowTimer(speed, 1));
        projective.Attributes.Add(new P_DeleteTimer(projective, 3));
        projectives.Add(projective);
    }
}
