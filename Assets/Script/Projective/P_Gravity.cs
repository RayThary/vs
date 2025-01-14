using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Gravity : IP_Attribute
{
    private Projective projective;
    private float gravity;

    public P_Gravity(Projective projective)
    {
        this.projective = projective;
        gravity = 9.8f;
    }

    public void Enter(Collider2D collider2D) { }
    public void Exit(Collider2D collider2D) { }
    public void LateEnter(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void LateUpdate() { }
    public void Update()
    {
        projective.transform.position += gravity * Time.deltaTime * Vector3.down;
        gravity += 9.8f * Time.deltaTime;
    }
}
