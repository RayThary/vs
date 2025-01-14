using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Follow : IP_Attribute
{
    private readonly Projective projective;
    private readonly Transform target;
    private Vector3 offset;

    public P_Follow(Projective projective, Vector2 offset, Transform target)
    {
        this.projective = projective;
        this.offset = offset;
        this.target = target;
    }

    public void Enter(Collider2D collider2D) { }
    public void Exit(Collider2D collider2D) { }
    public void LateEnter(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void LateUpdate() { }
    public void Update()
    {
        projective.transform.position = target.position + offset;
    }
}
