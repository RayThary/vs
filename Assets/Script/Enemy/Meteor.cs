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
    [SerializeField] private float attackDamage = 8;

    private bool meteorMove = false;

    private Transform meteor;
    private CircleCollider2D circle2d;

    void Start()
    {
        dangerTrs = transform.GetChild(0);
        meteor = transform.GetChild(1);
        meteor.gameObject.SetActive(false);
        circle2d = meteor.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        attackCheck();
        if (meteorMove)
        {
            meteor.transform.position += Vector3.down * meteorSpeed * Time.deltaTime;
          
            if (meteor.transform.position.y <= transform.position.y)
            {
                if (circle2d.IsTouchingLayers(LayerMask.GetMask("Player")))
                {
                        GameManager.Instance.GetPlayer.SelectCharacter.HP -= attackDamage;
                    GameManager.Instance.SetShakingWindow();
                    SoundManager.instance.SFXCreate(SoundManager.Clips.PlayerHit);

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
            if (scaleXY >= 0.3f)
            {
                if (meteorMove == false)
                {
                    meteorMove = true;
                    meteor.gameObject.SetActive(true);
                    Vector2 pos = transform.position;
                    pos.y += 4;
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
