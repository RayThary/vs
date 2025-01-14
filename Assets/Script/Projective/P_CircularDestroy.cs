using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_CircularDestroy : IP_Attribute
{
    //움직일 객체
    private Projective projective;
    //목표지점
    private Vector3 destination;
    //원의 중심점
    private Vector3 center;
    //원의 반지름
    private float radius;
    //현재 각도
    private float angle;
    //속도
    private float speed;

    public P_CircularDestroy(Projective projective, Vector3 destination, float speed)
    {
        this.projective = projective;
        this.destination = destination;
        this.speed = speed;
        //원의 중심은 객체와 목표지점의 중간값
        center = (destination + projective.transform.position) * 0.5f;
        radius = Vector3.Distance(center, destination);
        //시작 각도
        angle = Mathf.Atan2(projective.transform.position.y - center.y, projective.transform.position.x - center.x) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360;
    }

    public void Enter(Collider2D collider2D) { }
    public void Exit(Collider2D collider2D) { }
    public void LateEnter(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void LateUpdate() { }
    public void Update()
    {
        // 새로운 위치 계산
        float x = center.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = center.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        // 이동
        // 각도 업데이트
        if(projective.transform.position.x > destination.x)
            angle += speed * Time.deltaTime;
        else
            angle -= speed * Time.deltaTime;
        if (angle > 360f) angle -= 360f;

        projective.transform.position = new Vector3(x, y, 0);

        // 이동 방향으로 회전
        Vector3 direction = new Vector3(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0).normalized;
        projective.transform.right = -direction;

        if(Vector2.Distance(projective.transform.position, destination) < 0.1f)
        {
            Debug.Log("오브젝트풀링을 사용하지 않는 삭제");
            Object.Destroy(projective.gameObject);
        }
    }
}
