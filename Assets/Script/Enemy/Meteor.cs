using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private Transform dangerTrs;

    private float ratio;
    private float scaleXY;

    [SerializeField] private float attackSpeed;
    [SerializeField] private float meteorSpeed = 1;

    private bool attack = true;
    [SerializeField]
    private bool meteorMove = false;

    private Transform meteor;
    [SerializeField] private CircleCollider2D circle2d;
    void Start()
    {
        circle2d = GetComponent<CircleCollider2D>();
        dangerTrs = transform.GetChild(0);
        meteor = transform.GetChild(1);
        meteor.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        attackCheck();
        if (meteorMove)
        {
            meteor.transform.position += Vector3.down * meteorSpeed * Time.deltaTime;
            if (meteor.transform.position.y <= transform.position.y + 0.5f)
            {
                if (circle2d.IsTouchingLayers(LayerMask.GetMask("Player")))
                {
                    Debug.Log("플레이어 데미지");
                }
                dangerTrs.localScale = new Vector2(0, 0);
                ratio = 0;
                meteor.gameObject.SetActive(false);
                meteorMove = false;
                attack = true;
                PoolingManager.Instance.RemovePoolingObject(gameObject);
            }
        }
    }

    private void attackCheck()
    {
        if (attack)
        {

            ratio += Time.deltaTime * attackSpeed;
            scaleXY = Mathf.Lerp(0, 1, ratio);
            dangerTrs.localScale = new Vector3(scaleXY, scaleXY, 1);
            if (scaleXY >= 0.8f)
            {
                if (meteorMove == false)
                {
                    meteorMove = true;
                    meteor.gameObject.SetActive(true);
                    Vector2 pos = transform.position;
                    pos.y += 3;
                    meteor.transform.position = pos;
                }
            }

            if (scaleXY >= 1)
            {
                attack = false;
            }
        }
    }
}
