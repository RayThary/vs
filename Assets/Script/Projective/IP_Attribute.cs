using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IP_Attribute
{
    public void Enter(Collider2D collider2D);
    public void LateEnter(Collider2D collider2D);
    public void Exit(Collider2D collider2D);
    public void LateExit(Collider2D collider2D);
    public void Update();
    public void LateUpdate();   
}
