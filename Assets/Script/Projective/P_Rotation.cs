using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Rotation : IP_Attribute
{
    private readonly CircleCollider2D circleCollider2D;
    private float tick;
    private float timer;
    private readonly float frequency = 1.2f;          // 흔들림의 주기 (초당 반복 횟수)
    private readonly float amplitude = 0.1f;          // 흔들림의 강도 (진폭)
    private readonly float rotation;         // 얼마나 회전할지
    private readonly float count;            // 몇초 후에 다시 회전할지
    private readonly float start;            // 얼마나 회전 된 상태에서 시작할지

    public P_Rotation(Projective projective, float frequency, float amplitude, float rotation, float count, float start)
    {
        circleCollider2D = projective.GetComponent<CircleCollider2D>();
        tick = 0;
        timer = 0;
        this.frequency = frequency;
        this.amplitude = amplitude;
        this.rotation = rotation;
        this.count = count;
        this.start = start;
    }

    public void Enter(Collider2D collider2D)
    {

    }

    public void Exit(Collider2D collider2D)
    {

    }

    public void LateEnter(Collider2D collider2D)
    {

    }

    public void LateExit(Collider2D collider2D)
    {

    }

    public void LateUpdate()
    {

    }

    public void Update()
    {
        if(circleCollider2D != null)
        {
            timer += Time.deltaTime * frequency;
            if (timer < rotation)
            {
                circleCollider2D.offset = new Vector2(Mathf.Cos((start + timer) * Mathf.PI) * amplitude, Mathf.Sin((start + timer) * Mathf.PI) * amplitude);
            }
            else
            {
                tick += Time.deltaTime;
                if (tick >= count)
                {
                    timer = 0;
                    tick = 0;
                }
            }
        }
    }

}
