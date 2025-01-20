using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public enum AttackType
    {
        LineAttack,

    }
    [SerializeField] private AttackType attackType;
    [SerializeField] private float attackSpeed = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(attackType == AttackType.LineAttack)
        {
            transform.position += transform.forward * Time.deltaTime * attackSpeed;
        }
    }
}
