using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Rotation : IP_Attribute
{
    private readonly CircleCollider2D circleCollider2D;
    private float tick;
    private float timer;
    private readonly float frequency = 1.2f;          // ��鸲�� �ֱ� (�ʴ� �ݺ� Ƚ��)
    private readonly float amplitude = 0.1f;          // ��鸲�� ���� (����)
    private readonly float rotation;         // �󸶳� ȸ������
    private readonly float count;            // ���� �Ŀ� �ٽ� ȸ������
    private readonly float start;            // �󸶳� ȸ�� �� ���¿��� ��������

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
