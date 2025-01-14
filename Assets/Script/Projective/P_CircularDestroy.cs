using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_CircularDestroy : IP_Attribute
{
    //������ ��ü
    private Projective projective;
    //��ǥ����
    private Vector3 destination;
    //���� �߽���
    private Vector3 center;
    //���� ������
    private float radius;
    //���� ����
    private float angle;
    //�ӵ�
    private float speed;

    public P_CircularDestroy(Projective projective, Vector3 destination, float speed)
    {
        this.projective = projective;
        this.destination = destination;
        this.speed = speed;
        //���� �߽��� ��ü�� ��ǥ������ �߰���
        center = (destination + projective.transform.position) * 0.5f;
        radius = Vector3.Distance(center, destination);
        //���� ����
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
        // ���ο� ��ġ ���
        float x = center.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = center.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        // �̵�
        // ���� ������Ʈ
        if(projective.transform.position.x > destination.x)
            angle += speed * Time.deltaTime;
        else
            angle -= speed * Time.deltaTime;
        if (angle > 360f) angle -= 360f;

        projective.transform.position = new Vector3(x, y, 0);

        // �̵� �������� ȸ��
        Vector3 direction = new Vector3(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0).normalized;
        projective.transform.right = -direction;

        if(Vector2.Distance(projective.transform.position, destination) < 0.1f)
        {
            Debug.Log("������ƮǮ���� ������� �ʴ� ����");
            Object.Destroy(projective.gameObject);
        }
    }
}
