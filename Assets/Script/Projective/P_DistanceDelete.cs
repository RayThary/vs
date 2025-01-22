using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DistanceDelete : IP_Attribute
{
    private float m_Distance;
    private Vector2 m_Position;
    private Projective m_Projective;

    public P_DistanceDelete(float distance, Vector2 position, Projective projective)
    {
        m_Distance = distance;
        m_Position = position;
        m_Projective = projective;
    }

    public void Enter(Collider2D collider2D)
    {

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
        if(Vector2.Distance(m_Position, m_Projective.transform.position) > m_Distance)
        {
            PoolingManager.Instance.RemovePoolingObject(m_Projective.gameObject);
        }
    }
}
