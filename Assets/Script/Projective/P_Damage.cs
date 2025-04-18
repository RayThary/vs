using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Damage : IP_Attribute
{
    private readonly IAddon addon;
    private readonly float m_Damage;
    public float Damage { get => m_Damage; }

    public P_Damage(IAddon addon, float damage) 
    {
        this.addon = addon;
        m_Damage = damage;
    }
    public void Enter(Collider2D collider2D)
    {
        if(collider2D.TryGetComponent(out Enemy enemy))
        {
            float damage = m_Damage + GameManager.Instance.GetPlayer.Stat.AttackDamage;
            enemy.HP -= damage;
            enemy.EnemyKnockback(0.2f, 1);
            addon.Statistics += damage;
            GameManager.Instance.GetPlayer.SelectCharacter.HP += damage * GameManager.Instance.GetPlayer.Stat.LifeAbsorption * 0.5f;
            SoundManager.instance.SFXCreate(SoundManager.Clips.UnitHit);
        }
    }

    public void LateEnter(Collider2D collider2D) { }
    public void Exit(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void Update() { }
    public void LateUpdate() { }
}
