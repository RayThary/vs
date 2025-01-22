using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBoss : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rigd2d;

    private Vector2 targetVec;
    [SerializeField] private float speed = 2;
    private float basicSpeed;

    private bool targetChange = true;
    private bool attackCheck = false;

    private bool movingStop = false;
    private bool attackCoolChekc = false;
    private float timer = 0;
    private void Start()
    {
        player = GameManager.Instance.GetCharactor;
        rigd2d = GetComponent<Rigidbody2D>();
        basicSpeed = speed;
    }

    private void Update()
    {
        //moving();
        attack();

    }

    private void moving()
    {

        if (targetChange)
        {
            Vector2 maxVec = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            while (true)
            {
                float x = Random.Range(player.position.x - 5, player.position.x + 5);
                float y = Random.Range(player.position.y - 5, player.position.y + 5);
                targetVec = new Vector2(x, y);
                if ((-maxVec.x < x && x < maxVec.x) && (y > -maxVec.y && y < maxVec.y))
                {
                    float reDistance = Vector2.Distance(transform.position, targetVec);
                    if (reDistance > 3)
                    {
                        GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.CurvePatten, transform);
                        obj.GetComponent<CurvePatten>().SetPattenStart(transform.position);
                        targetChange = false;
                        break;
                    }
                }
            }

        }


        float playerDistance = Vector2.Distance(player.position, transform.position);
        if (playerDistance > 2)
        {
            speed = basicSpeed * 2f;
        }
        else
        {
            speed = basicSpeed;
        }



        if (player != null || movingStop == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetVec, speed * Time.deltaTime);
        }

        rigd2d.velocity = Vector2.zero;
    }

    private void attack()
    {

        if (!attackCoolChekc)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timer = 0;
                attackCoolChekc = true;
            }
        }

        if (attackCheck && attackCoolChekc)
        {
            attackCheck = false;
            attackCoolChekc = false;
            //АјАн
        }

    }
}
