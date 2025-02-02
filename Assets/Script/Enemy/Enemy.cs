using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum ExpType
    {
        Small,
        Medium,
        Large,
    }
    [SerializeField] private ExpType enemyExpType;

    private Rigidbody2D rigd2d;
    private Transform player;
    [SerializeField] private float speed = 1;
    public float Speed { get { return speed; } set { speed = value; } }
    private float tempSpeed;

    [SerializeField]
    private float hp;
    public float HP { get => hp; set => hp = value; }
    [SerializeField]
    private bool movingStop = false;

    private float enemySlowSpeed = 1;

    private bool knockBackCheck = false;
    private float knockBackPower = 1;
    private SpriteColorControl sprColorControl;

    private bool deathCheck = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            movingStop = true;
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Slow"))
        {
            speed = enemySlowSpeed;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            movingStop = false;
        }
    }
    private void Awake()
    {
        tempSpeed = speed;
    }

    private void Start()
    {
        player = GameManager.Instance.GetCharactor;
        sprColorControl = GetComponent<SpriteColorControl>();
        rigd2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (deathCheck == false)
        {
            enemyMoving();
        }
        enemyDie();
    }

    private void enemyMoving()
    {
        float dis = Vector2.Distance(player.position, transform.position);
        if (player != null && movingStop == false)
        {
            if (dis > 5)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * 2 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
        else if (movingStop && knockBackCheck)
        {
            if (dis > 5)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, -knockBackPower * 2 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, -knockBackPower * Time.deltaTime);
            }
        }


        rigd2d.velocity = Vector2.zero;
    }

    private void enemyDie()
    {
        if (deathCheck == false)
        {
            if (hp <= 0)
            {
                if (enemyExpType == ExpType.Small)
                {
                    PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.SmallExp, GameManager.Instance.GetPoolingTemp);
                }
                else if (enemyExpType == ExpType.Medium)
                {
                    PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.MediumExp, GameManager.Instance.GetPoolingTemp);
                }
                else
                {
                    PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.LargeExp, GameManager.Instance.GetPoolingTemp);
                }
                deathCheck = true;
            }
        }
    }

    /// <summary>
    /// 넉백 
    /// </summary>
    /// <param name="_kbTime">넉백시간 & 적 색깔변경시간 Defalt : 0.2f</param>
    /// <param name="_kbPower">넉백량  Defalt : 1</param>
    public void EnemyKnockback(float _kbTime, float _kbPower)
    {
        StartCoroutine(knockBack(_kbTime, _kbPower));
    }

    IEnumerator knockBack(float _kbTime, float _kbPower)
    {
        movingStop = true;
        knockBackCheck = true;
        sprColorControl.SetHit = true;
        sprColorControl.SetReturnTime = _kbTime;
        knockBackPower = _kbPower;
        yield return new WaitForSeconds(_kbTime);
        knockBackCheck = false;
        movingStop = false;
    }

    /// <summary>
    /// 슬로우
    /// </summary>
    /// <param name="_slowSpeed">속도</param>
    /// <param name="_slowTimer">느려지는시간</param>
    public void EnemySlow(float _slowSpeed, float _slowTimer)
    {
        StartCoroutine(slow(_slowSpeed, _slowTimer));
    }
    IEnumerator slow(float _slowSpeed, float _slowTimer)
    {
        speed = _slowSpeed;
        yield return new WaitForSeconds(_slowTimer);
        speed = tempSpeed;
    }
}
