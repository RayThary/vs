using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_EnterDelete : IP_Attribute
{
    private readonly Projective projective;

    public P_EnterDelete(Projective projective)
    {
        this.projective = projective;
    }

    public void Enter(Collider2D collider2D)
    {

    }

    public void LateEnter(Collider2D collider2D)
    {
        Debug.Log("������ƮǮ���� ����� ��Ȱ��ȭ �ʿ���");
        projective.gameObject.SetActive(false);
    }

    public void Exit(Collider2D collider2D)
    {

    }

    public void LateExit(Collider2D collider2D)
    {

    }

    public void Update()
    {

    }

    public void LateUpdate()
    {

    }
}
