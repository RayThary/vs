using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_EnemyAttackDelet : IP_Attribute
{

    private readonly Projective projective;
    public P_EnemyAttackDelet(Projective projective)
    {
        this.projective = projective;
    }

    public void Enter(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Wall"))
        {
            PoolingManager.Instance.RemovePoolingObject(projective.gameObject);
        }
    }

    public void Exit(Collider2D collider2D)
    {

    }

    public void LateEnter(Collider2D collider2D)
    {

    }

    public void LateExit(Collider2D collider2D)
    {

    }

    public void LateUpdate()
    {

    }

    public void Update()
    {

    }
}
