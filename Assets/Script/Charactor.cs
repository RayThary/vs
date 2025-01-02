using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{

    private float horizontal;
    private float vertical;

    private Rigidbody2D rigid2d;

    [SerializeField] private float characterSpeed = 3;

    private Transform mirroTrs;


    void Start()
    {
        GameManager.Instance.SetCharactor(transform);

        rigid2d = GetComponent<Rigidbody2D>();
        mirroTrs = transform.GetChild(0);
    }

    void Update()
    {
        characterMoving();
    }

    private void characterMoving()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontal == -1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        rigid2d.velocity = new Vector2(horizontal * characterSpeed, vertical * characterSpeed);

        Vector2 playerVec = transform.position;
        playerVec.x *= -1;
        mirroTrs.position = playerVec;
    }

    private void enemyCheck()
    {

    }
}
