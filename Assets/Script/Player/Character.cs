using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float horizontal;
    private float vertical;

    private Rigidbody2D rigid2d;

    [SerializeField] private float characterSpeed = 3;

    private Transform mirroTrs;

    [SerializeField]
    private float maxHp;
    public float MaxHP { get { return maxHp; } set {  maxHp = value; } }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return sprite; } }

    private float hp;
    public float HP { get => hp; set => hp = value; }

    private float maxSheild;
    public float MaxSheild { get {  return maxSheild; } set { maxSheild = value; } }

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
        mirroTrs = transform.GetChild(0);
    }

    // Update is called once per frame
    protected void Update()
    {
        characterMoving();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ButtonManager.Instance.ESC();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Skill();
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
        mirroTrs.position = playerVec;
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
