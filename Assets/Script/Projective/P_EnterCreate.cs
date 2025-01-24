using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enter시에 소환하는 스크립트
public class P_EnterCreate : IP_Attribute
{
    private readonly PoolingManager.ePoolingObject spawn;
    private readonly IAddon addon;

    public P_EnterCreate(PoolingManager.ePoolingObject spawn, IAddon addon)
    {
        this.spawn = spawn;
        this.addon = addon;
    }

    public void Enter(Collider2D collider2D)
    { 
        if(collider2D.GetComponent<Enemy>() != null)
        {
            Projective projective = PoolingManager.Instance.CreateObject(spawn, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
            projective.Init();

            projective.transform.position = collider2D.transform.position;
            projective.Attributes.Add(new P_DamageTimer(1, 1, addon));
            projective.Attributes.Add(new P_DeleteTimer(projective, 3.3f));
        }
    }
    public void Exit(Collider2D collider2D) { }
    public void LateEnter(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void LateUpdate() { }
    public void Update() { }
}
