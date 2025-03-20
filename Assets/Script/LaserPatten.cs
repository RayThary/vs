using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPatten : MonoBehaviour
{

    private BoxCollider2D box2d;
    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
        
    }
   

    public void AttackOn()
    {
        box2d.enabled = true;
    }
    public void AttackOff()
    {
        box2d.enabled = false;
        PoolingManager.Instance.RemovePoolingObject(gameObject);
    }
}
