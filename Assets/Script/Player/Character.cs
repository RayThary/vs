using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float horizontal;
    private float vertical;

    private Rigidbody2D rigid2d;

    [SerializeField] private float characterSpeed = 3;


    [SerializeField]
    private float maxHp;
    public float MaxHP { get { return maxHp; } set { maxHp = value; } }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return sprite; } }

    private float hp;
    public float HP { get => hp; set { if (!GameManager.Instance.IsInvincibility) hp = value; } }
    private float armor { get { return GameManager.Instance.GetPlayer.Stat.Armor; } }

    private float maxSheild;
    public float MaxSheild { get { return maxSheild; } set { maxSheild = value; } }

    [SerializeField]
    private float sheild;
    public float Sheild { get => sheild; set => sheild = value; }

    private float recover;
    private float sheildRecover;
    // Start is called before the first frame update
    protected void Start()
    {
        hp = maxHp;
        recover = Time.time;
        rigid2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected void Update()
    {
        characterMoving();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ButtonManager.Instance.ESC();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Skill();
        }
        if (hp <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    protected virtual void Skill() { }

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
    }

    public void SetHit(float _Damage)
    {
        hp -= Mathf.Max(_Damage - armor, 1);
    }

    public void HPRecovery()
    {
        if (recover + 1 < Time.time)
        {
            recover = Time.time;
            hp += GameManager.Instance.GetPlayer.Stat.HPRecovery;
        }
    }

    public void SheildRecover()
    {
        if (sheild != maxSheild && sheildRecover == 0)
        {
            sheildRecover = Time.time;
        }
        else if (sheild != maxSheild)
        {
            sheildRecover = 0;
        }
        else
        {
            if (sheildRecover + 5 < Time.time)
            {
                sheildRecover = Time.time;
                sheild += maxSheild;
            }
        }
    }
}
