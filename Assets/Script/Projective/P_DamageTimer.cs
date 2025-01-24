using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DamageTimer : IP_Attribute
{
    private readonly float damage;
    private readonly float cycle; 
    private readonly IAddon addon;
    private readonly List<TimerClass> enemies = new();
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
            enemies.Add(new TimerClass(0, enemy));
        }
    }
    public void Exit(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Enemy enemy))
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Enemy == enemy)
                {
                    enemies.Remove(new TimerClass(0, enemy));
                    return;
                }
            }
        }
    }
    public void LateEnter(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void LateUpdate() { }

    public void Update()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (Time.time > cycle + enemies[i].Time)
            {
                //최종 대미지
                float damage = this.damage + GameManager.Instance.GetPlayer.Stat.AttackDamage;
                //대미지 적용
                enemies[i].Enemy.HP -= damage;
                enemies[i].Enemy.EnemyKnockback(0.2f, 1);
                //무기가 준 피해량 측정
                addon.Statistics += damage;
                //생명령 흡수
                GameManager.Instance.GetPlayer.SelectCharacter.HP += damage * GameManager.Instance.GetPlayer.Stat.LifeAbsorption;
                enemies[i].Time = Time.time;
            }
        }
        
    }

    private class TimerClass
    {
        private float time;
        public float Time { get { return time; }set { time = value; } }  
        private Enemy enemy;
        public Enemy Enemy { get { return enemy; } set { enemy = value; } }
        public TimerClass(float time, Enemy enemy)
        {
            this.time = time;
            this.enemy = enemy;
        }
    }
}
