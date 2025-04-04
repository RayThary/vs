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
        MiniBoss,
        Boss,
    }
    [SerializeField] private ExpType enemyExpType;

    private Animator anim;
    private Rigidbody2D rigd2d;
    private Transform player;
    [SerializeField] private float speed = 1;
    public float Speed { get { return speed; } set { speed = value; } }
    private float tempSpeed;

    [SerializeField]
    private float hp;
    public float HP { get => hp; set => hp = value; }

    private bool movingStop = false;
    private bool playerHitCheck = false;

    private float enemySlowSpeed = 1;

    private bool knockBackCheck = false;
    private float knockBackPower = 1;
    private SpriteColorControl sprColorControl;

    private float timer = 1;

    private bool deathCheck = false;
    private int stageLevel = 1;
    private Vector3 enemyScale;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            movingStop = true;
            playerHitCheck = true;
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
            playerHitCheck = false;
            movingStop = false;
            timer = 1;
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
        anim = GetComponent<Animator>();
        enemyScale = transform.localScale;
        SetHp();
    }



    private void Update()
    {
        playerResetCheck();

        enemyHpCheck();
        enemyDie();
        playerDamege();
        if (enemyExpType != ExpType.Boss)
        {
            enemyMoving();
        }
    }
    private void playerResetCheck()
    {
        if (player == null)
        {
            player = GameManager.Instance.GetCharactor;
        }
    }
    private void enemyHpCheck()
    {
        if (stageLevel != GameManager.Instance.GetStageLevel)
        {
            stageLevel = GameManager.Instance.GetStageLevel;
            SetHp();
        }
    }
    private void enemyMoving()
    {
        if (deathCheck)
        {
            return;
        }
        float dis = Vector2.Distance(player.position, transform.position);
        if (player != null && movingStop == false)
        {
            if (dis < 10 || enemyExpType == ExpType.MiniBoss)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * 1.5f * Time.deltaTime);
            }
        }
        else if (movingStop && knockBackCheck)
        {
            if (dis > 5)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, -knockBackPower * 1.5f * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, -knockBackPower * Time.deltaTime);
            }
        }

        if (player != null)
        {
            if (transform.position.x > player.transform.position.x)
            {

                transform.localScale = enemyScale;
            }
            else
            {

                transform.localScale = new Vector2(-enemyScale.x, enemyScale.y);
            }
        }

        rigd2d.velocity = Vector2.zero;
    }

    private void playerDamege()
    {
        if (playerHitCheck)
        {
            timer += Time.deltaTime;
            if (timer > 0.2f)
            {
                float damage;
                damage = enemyExpType switch
                {
                    ExpType.Small => 1,
                    ExpType.Medium => 1.5f,
                    ExpType.Large => 2,
                    ExpType.MiniBoss => 3,
                    ExpType.Boss => 5,
                    _ => 0,
                };
                GameManager.Instance.GetPlayer.SelectCharacter.SetHit(damage);
                GameManager.Instance.SetShakingWindow();
                SoundManager.instance.SFXCreate(SoundManager.Clips.PlayerHit);
                timer = 0;
            }
        }
    }
    private void enemyDie()
    {
        if (deathCheck == false)
        {
            if (hp <= 0)
            {
                GameObject obj;
                if (enemyExpType == ExpType.Small)
                {
                    obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.SmallExp, GameManager.Instance.GetPoolingTemp);
                    obj.transform.position = transform.position;
                }
                else if (enemyExpType == ExpType.Medium)
                {
                    obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.MediumExp, GameManager.Instance.GetPoolingTemp);
                    obj.transform.position = transform.position;
                }
                else if ((enemyExpType == ExpType.Large))
                {
                    obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.LargeExp, GameManager.Instance.GetPoolingTemp);
                    obj.transform.position = transform.position;
                }
                else
                {
                    obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.BossExp, GameManager.Instance.GetPoolingTemp);
                    obj.transform.position = transform.position;
                }

                deathCheck = true;
                anim.SetTrigger("Death");
            }
        }
    }

    private void SetHp()
    {
        hp = enemyExpType switch
        {
            ExpType.Small => GameManager.Instance.GetStageLevel * 10,
            ExpType.Medium => 50 + GameManager.Instance.GetStageLevel * 20,
            ExpType.Large => 100 + GameManager.Instance.GetStageLevel * 40,
            ExpType.MiniBoss => 1000,
            ExpType.Boss => 2000,
            _ => 20
        };
    }

    /// <summary>
    /// 넉백 
    /// </summary>
    /// <param name="_kbTime">넉백시간 & 적 색깔변경시간 Defalt : 0.2f</param>
    /// <param name="_kbPower">넉백량  Defalt : 1</param>
    public void EnemyKnockback(float _kbTime, float _kbPower)
    {
        if (sprColorControl != null)
        {
            StartCoroutine(knockBack(_kbTime, _kbPower));
        }
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

    private void EnemyDeath()
    {
        PoolingManager.Instance.RemovePoolingObject(gameObject);
        SetHp();
        deathCheck = false;
        knockBackCheck = false;
        movingStop = false;
    }
}
