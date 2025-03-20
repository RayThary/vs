using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    
    [SerializeField] private float attackDamage = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.GetPlayer.SelectCharacter.HP -= attackDamage;
            GameManager.Instance.SetShakingWindow();
            SoundManager.instance.SFXCreate(SoundManager.Clips.PlayerHit);
        }
    }
}
