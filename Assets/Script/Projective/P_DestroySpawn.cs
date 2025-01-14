using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DestroySpawn : IP_Attribute
{
    private readonly Projective game;
    private readonly Projective spawn;

    public P_DestroySpawn(Projective game, Projective spawn)
    {
        this.game = game;
        this.spawn = spawn;
    }

    public void Enter(Collider2D collider2D) { }
    public void Exit(Collider2D collider2D) { }
    public void LateEnter(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void LateUpdate() { }
    public void Update()
    {
        //game������Ʈ�� ��Ȱ��ȭ �Ǿ����� �ѹ� ȣ��
        if(!game.gameObject.activeSelf)
        {
            //�ٽ� ȣ����� �ʵ��� �����
            game.Attributes.Remove(this);
            //�� ������Ʈ ����
            Projective projective = Object.Instantiate(spawn);
            projective.Init();

            projective.transform.position = game.transform.position; 
            projective.Attributes.Add(new P_DeleteTimer(projective, 10));
        }
    }
}
