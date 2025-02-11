using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLastBoss : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rigd2d;
    private Animator anim;

    private Vector2 targetVec;
    [SerializeField] private float speed = 2;
    private float basicSpeed;

    private bool movinStop = false;

    [SerializeField]
    private float hp = 100;

    private bool attackCheck = false;
    private bool attackCoolChekc = false;
    private float timer = 0;
    [SerializeField] private bool SlowInPlayer = false;

    private Vector3 mapSize;
    private void Start()
    {
        player = GameManager.Instance.GetCharactor;
        rigd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        basicSpeed = speed;

        mapSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - 200, Screen.height - 200));
    }

    private void Update()
    {
        moving();
        attack();

    }

    private void moving()
    {

        float targetDistance = Vector2.Distance(transform.position, targetVec);

        if (SlowInPlayer)
        {
            float playerDistance = Vector2.Distance(player.position, transform.position);
            if (playerDistance > 2)
            {
                speed = basicSpeed * 2f;
            }
            else
            {
                speed = basicSpeed;
            }
        }

        if (targetDistance <= 0.1f)
        {
            if (attackCheck == false)
            {
                movinStop = true;
                anim.SetTrigger("Attack");
                attackCheck = true;
            }
        }

        if (player != null)
        {
            if (movinStop)
            {
                return;
            }

           
            transform.position = Vector3.MoveTowards(transform.position, targetVec, speed * Time.deltaTime);
        }

        rigd2d.velocity = Vector2.zero;
    }

    private void attack()
    {

        if (!attackCoolChekc)
        {
            timer += Time.deltaTime;
            if (timer > 3.5f)
            {
                timer = 0;
                attackCoolChekc = true;
                StartCoroutine(meteor(8));
            }
        }
    }

    private void death()
    {
        if (hp <= 0)
        {
            anim.SetTrigger("Death");
        }
    }

    //소환될 개수
    IEnumerator meteor(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = SetSpawnPos();
            yield return new WaitForSeconds(0.5f);
            GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Meteor, GameManager.Instance.GetPoolingTemp);
            obj.transform.position = spawnPos;
        }
        attackCoolChekc = false;
    }

    private Vector3 SetSpawnPos()
    {
        float posX = Random.Range(-mapSize.x, mapSize.x);
        float posY = Random.Range(-mapSize.y, mapSize.y);

        return new Vector3(posX, posY, 0);
    }

    //애니메이션 이벤트
    private void curveAttack()
    {

        Vector2 maxVec = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        while (true)
        {
            float x = Random.Range(player.position.x - 3, player.position.x + 4);
            float y = Random.Range(player.position.y - 3, player.position.y + 4);
            targetVec = new Vector2(x, y);
            if ((-maxVec.x < x && x < maxVec.x) && (y > -maxVec.y && y < maxVec.y))
            {
                float reDistance = Vector2.Distance(transform.position, targetVec);
                if (reDistance > 3)
                {
                    GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.CurvePatten, transform);
                    obj.GetComponent<CurvePatten>().SetPattenStart(transform.position);
                    break;
                }
            }
        }
        if (targetVec.x > transform.position.x)
        {
            transform.localScale = new Vector2(-2, 2);
        }
        else if (targetVec.x < transform.position.x)
        {
            transform.localScale = new Vector2(2, 2);
        }
        movinStop = false;
        attackCheck = false;
    }
    //애니메이션 이벤트
    private void enemyDeath()
    {
        PoolingManager.Instance.RemovePoolingObject(gameObject);
    }
}
