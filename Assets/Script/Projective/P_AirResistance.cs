using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AirResistance : IP_Attribute
{
    private readonly P_Move p_Move;
    float velocity = 0f;
    float smoothTime = 0.8f; // 감속 속도

    public P_AirResistance(P_Move move)
    {
        p_Move = move;
    }

    public void Enter(Collider2D collider2D)
    {

    }

    public void LateEnter(Collider2D collider2D)
    {

    }

    public void Exit(Collider2D collider2D)
    {

    }

    public void LateExit(Collider2D collider2D)
    {

    }

    public void Update()
    {

    }
    public void LateUpdate()
    {
        p_Move.Speed = Mathf.SmoothDamp(p_Move.Speed, 0f, ref velocity, smoothTime);
    }
}
