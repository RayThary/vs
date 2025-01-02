using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigd2d;
    private Transform player;
    [SerializeField] private float speed = 1;

    [SerializeField]
    private float hp;
    public float HP { get => hp; set => hp = value; }

    private bool movingStop = false;


    private float enemySlowSpeed = 1;
    private float enemySlowTime = 0.0f;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            movingStop = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //´ë¹ÌÁö?
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Slow"))
        {
            speed = enemySlowSpeed;
        }
    }

    private void Start()
    {
        player = GameManager.Instance.GetCharactor;
        rigd2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyMoving();
    }

    private void enemyMoving()
    {
        if (player != null && movingStop == false)
        {
            float dis = Vector2.Distance(player.position, transform.position);

            if (dis > 5)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * 3 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            }
        }
        rigd2d.velocity = Vector2.zero;
    }


    public void EnemyKnockback()
    {

    }

}
