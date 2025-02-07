using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AutoTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    public Transform GetTarget { get { return target; } }

    private Transform mouseTrs;


    void Start()
    {
        mouseTrs = transform.Find("MousePoint");
    }

    void Update()
    {
        if (GameManager.Instance.GetPlayer.Setting.Auto)
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
        //RaycastHit2D[] hit = Physics2D.OverlapCircleAll(transform.position, 200, LayerMask.GetMask("Enemy"));
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 200, LayerMask.GetMask("Enemy"));
        Transform result = null;

        float basicDif = float.MaxValue;

        foreach (Collider2D target in hit)
        {
            
            Vector3 targetPos = target.transform.position;

            float dif = (GameManager.Instance.GetCharactor.position - targetPos).sqrMagnitude;
            if (dif < basicDif)
            {
                basicDif = dif;
                result = target.transform;
            }

        }

        if (result != null)
        {
            return result.position;
        }
        else
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

    }

    private Transform FindMouseTarget()
    {
        mouseTrs.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return null;
    }
}
