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

    private float maxHp;
    private float hp => GetComponent<Enemy>().HP;


    private bool attackCheck = false;
    private bool meteroAttackCoolChekc = false;
    private float curveTimer = 0;

    private bool laserPatten = false;

    private bool laserAttackCheck = false;
    private bool laserCheck = false;
    private float laserTimer = 0;

    [SerializeField] private bool SlowInPlayer = false;

    private Vector3 mapSize;
    private void Start()
    {
        player = GameManager.Instance.GetCharactor;
        rigd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        basicSpeed = speed;
        maxHp = hp;
        mapSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - 200, Screen.height - 200));
    }

    private void Update()
    {
        moving();
        attack();
        if (Input.GetKeyDown(KeyCode.V))
        {
            anim.SetTrigger("LaserAttack");
        }
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

        if (!laserCheck)
        {
            if (targetDistance <= 0.1f)
            {
                if (attackCheck == false)
                {
                    movinStop = true;
                    anim.SetTrigger("Attack");
                    attackCheck = true;
                }
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

        if (!meteroAttackCoolChekc)
        {
            curveTimer += Time.deltaTime;
            if (curveTimer > 3)
            {
                curveTimer = 0;
                meteroAttackCoolChekc = true;
                int meteorCount = Random.Range(6, 10);
                StartCoroutine(meteor(meteorCount));
            }
        }

        if (!laserAttackCheck && laserPatten)
        {
            laserTimer += Time.deltaTime;

            if (laserTimer > 3)
            {
                Debug.Log("작동");
                laserAttackCheck = true;
                laserCheck = true;
                anim.SetTrigger("LaserAttack");
                laserTimer = 0;
            }

        }

        if (hp <= maxHp * 0.5f && !laserPatten)
        {
            laserPatten = true;
        }

    }



    //소환될 개수
    IEnumerator meteor(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = SetSpawnPos();
            yield return new WaitForSeconds(0.3f);
            GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Meteor, GameManager.Instance.GetPoolingTemp);
            obj.transform.position = spawnPos;
        }
        meteroAttackCoolChekc = false;
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
            transform.localScale = new Vector2(-3, 3);
        }
        else if (targetVec.x < transform.position.x)
        {
            transform.localScale = new Vector2(3, 3);
        }
        movinStop = false;
        attackCheck = false;
    }

    private void laserAttack()
    {
        Debug.Log("소환");
        GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.BossLaser, GameManager.Instance.GetEnemyPoolingTemp);
        Vector3 dir = (player.position - transform.position).normalized;
        obj.transform.position = transform.position + (dir * 1);

        dir = player.position - obj.transform.position;
        dir = dir.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.Euler(0, 0, angle);


    }

    private void laserEnd()
    {
        laserAttackCheck = false;
        laserCheck = false;
        Debug.Log("레이저끝");
    }
    //애니메이션 이벤트
    private void enemyDeath()
    {
        PoolingManager.Instance.RemovePoolingObject(gameObject);
    }
}
