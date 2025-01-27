using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    private readonly float damage;

    public Magic_10(Player player)
    {
        this.player = player;
        description = "플레이어 주위에 피해를 입히며 상대를 느려지게 한다";
        this.player = player;
        speed = 0.3f;
        damage = 1;
        level = 0;
    }

    public void Addon()
    {
        Fire();
        level = 1;
    }

    public void LevelUp()
    {
        level++;
        speed += 0.1f;
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
        //모든 발사체 삭제
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));
        projectives.Clear();
    }

    public void Update()
    {

    }

    private void Fire()
    {
        //투사체 설정
        Debug.Log("오브젝트 풀링을 사용하지 않는 생성");
        Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic10, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
        projective.Init();

        projective.transform.position = player.SelectCharacter.transform.position;
        projective.Attributes.Add(new P_Damage(this, damage));
        projective.Attributes.Add(new P_SlowTimer(speed, 1));
        projectives.Add(projective);
    }
}
