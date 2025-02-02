using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_CircularDestroy : IP_Attribute
{
    //������ ��ü
    private Projective projective;
    private readonly PoolingManager.ePoolingObject spawn;
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
    private bool axis;

    public P_CircularDestroy(Projective projective, PoolingManager.ePoolingObject spawn, Vector3 destination, float speed, float max)
    {
        this.projective = projective;
        this.spawn = spawn;
        this.destination = destination;
        if(destination.magnitude > max)
        {
            destination = (destination - projective.transform.position).normalized * max;
        }
        
        this.speed = speed;
        //���� �߽��� ��ü�� ��ǥ������ �߰���
        center = (destination + projective.transform.position) * 0.5f;
        radius = Vector3.Distance(center, destination);
        //���� ����
        angle = Mathf.Atan2(projective.transform.position.y - center.y, projective.transform.position.x - center.x) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360;
        if (projective.transform.position.x > destination.x)
            axis = true;
        else
            axis = false;
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
        if(axis)
            angle += speed * Time.deltaTime;
        else
            angle -= speed * Time.deltaTime;
        if (angle > 360f) angle -= 360f;

        projective.transform.position = new Vector3(x, y, 0);

        // �̵� �������� ȸ��
        projective.transform.eulerAngles = new Vector3(0, 0, angle - 100);
        if (Vector2.Distance(projective.transform.position, destination) < 0.1f)
        { 
            Projective p = PoolingManager.Instance.CreateObject(spawn, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
            p.Init();

            p.transform.position = projective.transform.position;
            p.Attributes.Add(new P_DeleteTimer(p, 10));
            PoolingManager.Instance.RemovePoolingObject(projective.gameObject);
        }
    }
}
