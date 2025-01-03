using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DamageTimer : IP_Attribute
{
    private readonly float damage;
    private readonly float cycle; 
    private readonly IAddon addon;
    private float timer;
    private readonly List<Enemy> enemies = new();
    public P_DamageTimer(float damage, float cycle, IAddon addon)
    {
        this.damage = damage;
        this.cycle = cycle;
        this.addon = addon;
    }

    public void Enter(Collider2D collider2D)
    {
        if(collider2D.TryGetComponent(out Enemy enemy))
        {
            if(enemies.Count == 0)
                timer = 0;
            enemies.Add(enemy);
        }
    }
    public void Exit(Collider2D collider2D) { }
    public void LateEnter(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void LateUpdate() { }

    public void Update()
    {
        if(Time.time > cycle + timer)
        {
            //최종 대미지
            float damage = this.damage + GameManager.Instance.GetPlayer.Stat.AttackDamage;
            //대미지 적용
            enemies.ForEach(enemy => enemy.HP -= damage);
            //무기가 준 피해량 측정
            addon.Statistics += damage;
            //생명령 흡수
            GameManager.Instance.GetPlayer.SelectCharacter.HP += damage * GameManager.Instance.GetPlayer.Stat.LifeAbsorption;
            timer = Time.time;
        }
    }
}
