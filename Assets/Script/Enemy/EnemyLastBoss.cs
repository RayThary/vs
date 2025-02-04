using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLastBoss : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rigd2d;

    private Vector2 targetVec;
    [SerializeField] private float speed = 2;
    private float basicSpeed;

    private bool targetChange = true;
    private bool attackCheck = false;

    //private bool movingStop = false;
    private bool attackCoolChekc = false;
    private float timer = 0;
    [SerializeField] private bool SlowInPlayer = false;

    private Vector3 mapSize;
    private void Start()
    {
        player = GameManager.Instance.GetCharactor;
        rigd2d = GetComponent<Rigidbody2D>();
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

        if (targetChange)
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
                        targetChange = false;
                        break;
                    }
                }
            }

        }
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

        if (targetDistance < 0.5f)
        {
            targetChange = true;
            attackCheck = true;
        }


        if (player != null)
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
            if (timer > 3.5f)
            {
                timer = 0;
                attackCoolChekc = true;
                StartCoroutine(meteor(8));
            }
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
}
