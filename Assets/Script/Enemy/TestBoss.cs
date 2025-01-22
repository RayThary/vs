using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rigd2d;
    private Animator anim;

    private Vector2 targetVec;
    private Vector2 maxVec;

    [SerializeField] private float speed = 2;
    private float basicSpeed;

    private bool targetChange = false;
    private bool attackCheck = false;

    private bool movingStop = false;
    private bool attackCoolChekc = false;
    private float timer = 0;

    [SerializeField] private bool SlowInPlayer = false;

    private float dirX;
    private void Start()
    {
        player = GameManager.Instance.GetCharactor;
        rigd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        basicSpeed = speed;
        maxVec = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - 50, Screen.height - 100));


    }

    private void Update()
    {
        moving();
        attack();

    }

    private void moving()
    {
        if (player != null && movingStop == false)
        {
            targetVec = GameManager.Instance.GetCharactor.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetVec, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetVec, speed * 3 * Time.deltaTime);
        }

        rigd2d.velocity = Vector2.zero;
    }


    private void attack()
    {

        if (!attackCoolChekc)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                timer = 0;
                attackCoolChekc = true;
                targetChange = true;
                movingStop = true;
            }
        }

        if (targetChange)
        {
            float x;
            float y;
            if (player.position.x > transform.position.x)
            {
                x = Random.Range(player.position.x + 1, player.position.x + 3);
                if (x >= maxVec.x)
                {
                    x = maxVec.x;
                }
            }
            else
            {
                x = Random.Range(player.position.x - 1, player.position.x - 3);
                if (-maxVec.x >= x)
                {
                    x = -maxVec.x;
                }
            }
            y = Random.Range(player.position.y - 3, player.position.y + 3);
            if (y >= maxVec.y)
            {
                y = maxVec.y;
            }
            else if (y <= -maxVec.y)
            {
                y = -maxVec.y;
            }

            targetVec = new Vector2(x, y);


            targetChange = false;
            attackCheck = true;
        }

        if (attackCheck)
        {
            float dis = Vector2.Distance(transform.position, targetVec);
            if (dis == 0)
            {
                if (transform.position.x > player.position.x)
                {
                    dirX = -1;
                }
                else
                {
                    dirX = 1;
                }
                anim.SetTrigger("AttackType1");
                attackCheck = false;
            }
        }
        //attackCoolChekc = false;
    }

    private void AttackType1()
    {
        float dirY = -1.5f;



        for (int i = 0; i < 7; i++)
        {
            Vector2 dir = new Vector2(dirX, dirY);
            GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.BossAttack, GameManager.Instance.GetPoolingTemp);
            obj.transform.position = transform.position;
            Projective tempBullet = obj.GetComponent<Projective>();
            tempBullet.Init();
            tempBullet.Attributes.Add(new P_Move(tempBullet, dir, 3));
            tempBullet.Attributes.Add(new P_EnemyAttackDelet(tempBullet));

            dirY += 0.5f;
        }
    }
    private void de()
    {
        attackCoolChekc = false;
        movingStop = false;
    }
    IEnumerator AttackPatten()
    {

        yield return new WaitForSeconds(0.5f);
    }
}
