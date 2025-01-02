using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{

    //풀링 처리해줄것!!!!!

    public enum ExpType
    {
        Small,
        Medium,
        Large,
    }
    [SerializeField] private ExpType m_expType;
    private Player player;
    private Transform playerTrs;

    private float playerDis;
    private bool playerCheck = false;

    private float timer = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (m_expType == ExpType.Small)
            {
                player.AddExp(1);
                Debug.Log("플레이어 Exp 1 증가");
            }
            else if (m_expType == ExpType.Medium)
            {
                player.AddExp(2);
                Debug.Log("플레이어 Exp 2 증가");
            }
            else if (m_expType == ExpType.Large)
            {
                player.AddExp(3);
                Debug.Log("플레이어 Exp 3 증가");
            }

            Destroy(gameObject);
        }
    }
    void Start()
    {
        player = GameManager.Instance.GetPlayer;
        playerTrs = GameManager.Instance.GetCharactor;
        playerDis = GameManager.Instance.GetExpDistance;
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(playerTrs.position, transform.position);
        if (dis < playerDis)
        {
            playerCheck = true;

        }

        if (playerCheck)
        {
            timer += Time.deltaTime;
            if (timer > 0.1)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTrs.position, 10 * Time.deltaTime);

            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTrs.position, -5 * Time.deltaTime);
            }

        }


    }
}
