using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DeleteTimer : IP_Attribute
{
    private Projective projective;
    private float timer;
    private float f;
    
    public P_DeleteTimer(Projective projective, float timer)
    {
        this.projective = projective;
        this.timer = timer;
        f = Time.time;
    }

    public void Enter(Collider2D collider2D) { }
    public void Exit(Collider2D collider2D) { }
    public void LateEnter(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void LateUpdate() { }
    public void Update()
    {
        if(f + timer < Time.time)
        {
            PoolingManager.Instance.RemovePoolingObject(projective.gameObject);
        }
    }
}
