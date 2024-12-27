using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Move : IP_Attribute
{
    private readonly Projective projective;
    private Vector2 dir;
    public Vector2 Direction { get { return dir; } set { dir = value; } }
    private float speed;    
    public float Speed { get { return speed; } set {  speed = value; } }

    public P_Move(Projective projective, Vector2 dir, float speed)
    {
        this.projective = projective;
        this.dir = dir;
        this.speed = speed;
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
        projective.transform.position += Time.deltaTime * speed * (Vector3)Direction;
    }

    public void LateUpdate()
    {

    }
}
