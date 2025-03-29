using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_EnemyDamage : IP_Attribute
{
    private readonly float m_Damage;
    public float Damage { get => m_Damage; }

    public P_EnemyDamage(float damage)
    {
        m_Damage = damage;
    }
    public void Enter(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Character character))
        {
            character.SetHit(m_Damage);
            SoundManager.instance.SFXCreate(SoundManager.Clips.PlayerHit);
        }
    }

    public void LateEnter(Collider2D collider2D) { }
    public void Exit(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void Update() { }
    public void LateUpdate() { }
}

