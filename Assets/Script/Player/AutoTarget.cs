using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AutoTarget : MonoBehaviour
{   
    public bool autoTarget = false;
    [SerializeField] private Transform target;
    public Transform GetTarget {  get { return target; } }

    private Transform mouseTrs;


    void Start()
    {
        mouseTrs = transform.Find("MousePoint");
    }
    
    void Update()
    {
        if (autoTarget)
        {   
            target.position = FindAutoTarget();
        }
        else
        {
            target.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
    }

    //제일가까운적을  리턴해줌
    private Vector3 FindAutoTarget()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, 200, Vector2.zero, 0, LayerMask.GetMask("Enemy"));

        Transform result = null;
        float diff = 50;

        foreach (RaycastHit2D target in hit)
        {
            Vector3 pos = transform.position;
            Vector3 targetPos = target.transform.position;

            float dif = (transform.position - targetPos).sqrMagnitude;
            if (dif < diff)
            {
                diff = dif;
                result = target.transform;
            }

        }

        return result.position;
    }

    private Transform FindMouseTarget()
    {
        mouseTrs.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return null;
    }
}
