using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{

    public enum ExpType
    {
        Small,
        Medium,
        Large,
    }
    [SerializeField]
    private ExpType m_expType;
    private Player player;
    private Transform playerTrs;
    private SpriteRenderer spr;

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
            }
            else if (m_expType == ExpType.Medium)
            {
                player.AddExp(2);
                
            }
            else if (m_expType == ExpType.Large)
            {
                player.AddExp(3);
            }

            Destroy(gameObject);
        }
    }
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        player = GameManager.Instance.GetPlayer;
        playerTrs = GameManager.Instance.GetCharactor;
        playerDis = GameManager.Instance.GetExpDistance;

        if (m_expType == ExpType.Small)
        {
            spr.color = Color.green;
        }
        else if (m_expType == ExpType.Medium)
        {
            spr.color = Color.yellow;
        }
        else
        {
            spr.color = Color.cyan;
        }
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
