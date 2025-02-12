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
        Boss,
    }
    [SerializeField]
    private ExpType m_expType;
    private Player player;
    private Transform playerTrs;

    private float playerDis;
    private bool playerCheck = false;

    private int basicExp = 0;

    private float timer = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            basicExp = m_expType switch
            {
                ExpType.Small => 5 * GameManager.Instance.GetStageLevel,
                ExpType.Medium => 10 + (GameManager.Instance.GetStageLevel * 10),
                ExpType.Large => 20 + ((GameManager.Instance.GetStageLevel - 1) * 20),
                ExpType.Boss => 0,
                _ => 1
            };

            if (basicExp == 0)
            {
                player.CardSelect.On = true;
            }
            else
            {
                player.AddExp(basicExp);
            }

            Destroy(gameObject);
        }
    }
    void Start()
    {
        player = GameManager.Instance.GetPlayer;
        playerTrs = GameManager.Instance.GetCharactor;
        playerDis = GameManager.Instance.GetExpDistance;


        //basicExp = m_expType switch
        //{
        //    ExpType.Small => 5 * GameManager.Instance.GetStageLevel,
        //    ExpType.Medium => 10 + (GameManager.Instance.GetStageLevel * 10),
        //    ExpType.Large => 20 + ((GameManager.Instance.GetStageLevel - 1) * 20),
        //    _ => 1
        //};



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
