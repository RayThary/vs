using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Slow : IP_Attribute
{
    private float speed;

    public P_Slow(float speed) { this.speed = speed; }

    public void Enter(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Enemy enemy))
        {
            enemy.Speed -= speed;
        }
    }

    public void Exit(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Enemy enemy))
        {
            enemy.Speed += speed;
        }
    }

    public void LateEnter(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void LateUpdate() { }
    public void Update() { }
}
