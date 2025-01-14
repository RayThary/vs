using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Rotation : IP_Attribute
{
    private Projective projective;
    private float speed;

    public P_Rotation(Projective projective, float speed)
    {
        this.projective = projective;
        this.speed = speed;
    }

    public void Enter(Collider2D collider2D) { }
    public void Exit(Collider2D collider2D) { }
    public void LateEnter(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void LateUpdate() { }
    public void Update()
    {
        projective.transform.eulerAngles += new Vector3(0, 0, speed * Time.deltaTime);
    }
}
