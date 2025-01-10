using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigd2d;
    private Transform player;
    [SerializeField] private float speed = 1;
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField]
    private float hp;
    public float HP { get => hp; set => hp = value; }
    [SerializeField]
    private bool movingStop = false;

    private float enemySlowSpeed = 1;

    private bool knockBackCheck = false;
    private float knockBackPower = 1;
    private SpriteColorControl sprColorControl;

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

    protected void Start()
    {
        player = GameManager.Instance.GetCharactor;
        sprColorControl = GetComponent<SpriteColorControl>();
        rigd2d = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        enemyMoving();
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

    /// <summary>
    /// ³Ë¹é 
    /// </summary>
    /// <param name="_kbTime">³Ë¹é½Ã°£ & Àû »ö±òº¯°æ½Ã°£ Defalt : 0.2f</param>
    /// <param name="_kbPower">³Ë¹é·®  Defalt : 1</param>
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

}
