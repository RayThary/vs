using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static List<Enemy> enemyList = new();


    private Rigidbody2D rigd2d;
    private Transform player;
    [SerializeField] private float speed = 1;

    [SerializeField]
    private float hp;
    public float HP { get => hp; set => hp = value; }

    private void Start()
    {
        enemyList.Add(this);
        //rigd2d = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.GetPlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        enemyMoving();
    }

    private void enemyMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
