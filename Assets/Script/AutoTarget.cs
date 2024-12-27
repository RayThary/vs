using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTarget : MonoBehaviour
{   
    public bool autoTarget = false;
    [SerializeField] private Transform target;


    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (autoTarget)
        {
            target = FindTarget();
        }
        else
        {

        }
        
    }

    //제일가까운적을  리턴해줌
    private Transform FindTarget()
    {

        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, 200, Vector2.zero, 0, LayerMask.GetMask("Monster"));

        Transform result = null;
        float diff = 50;

        foreach (RaycastHit2D target in hit)
        {
            Vector3 pos = transform.position;
            Vector3 targetPos = target.transform.position;

            float dif = Vector3.Distance(pos, targetPos);
            if (dif < diff)
            {
                diff = dif;
                result = target.transform;
            }

        }




        return result;
    }
}
