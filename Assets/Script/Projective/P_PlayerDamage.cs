using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_PlayerDamage : IP_Attribute
{
    private readonly IAddon addon;
    private readonly float m_Damage;
    public float Damage { get => m_Damage; }

    public P_PlayerDamage(IAddon addon, float damage)
    {
        this.addon = addon;
        m_Damage = damage;
    }
    public void Enter(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Character character))
        {
            float damage = m_Damage + GameManager.Instance.GetPlayer.Stat.AttackDamage;
            character.HP -= damage;
            addon.Statistics += damage;
            GameManager.Instance.GetPlayer.SelectCharacter.HP += damage * GameManager.Instance.GetPlayer.Stat.LifeAbsorption;
        }
    }

    public void LateEnter(Collider2D collider2D) { }
    public void Exit(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void Update() { }
    public void LateUpdate() { }
}
