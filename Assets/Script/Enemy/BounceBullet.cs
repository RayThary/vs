using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BounceBullet : MonoBehaviour
{
    public enum BulletType
    {
        NormalBullet,
        SplitBullet,
    }
    [SerializeField] private BulletType bulletType;


    [SerializeField] private float speed = 2;
    [SerializeField] private float damage = 5;
    private Vector3 dir;

    private int bounceCount = 0;
    private int bounceMax = 2;
    //방향 , 속도 , 공격력, 튕기는회수
    public (Vector3 dir, float speed, float damage, int bounceCount) SetDir
    {
        set
        {
            dir = value.dir;
            speed = value.speed;
            damage = value.damage;
            bounceMax = value.bounceCount;
        }
    }

    private Vector2[] dirs = new Vector2[]
    {
        new Vector2(1, 0),
        new Vector2(1, 1),
        new Vector2(0, 1),
        new Vector2(-1, 1),
        new Vector2(-1, 0),
        new Vector2(-1, -1),
        new Vector2(0, -1),
        new Vector2(1, -1)
    };

    private float timer = 0.0f;
    private float bulletSplitTimer = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.GetPlayer.SelectCharacter.SetHit(damage);
            GameManager.Instance.SetShakingWindow();
            SoundManager.instance.SFXCreate(SoundManager.Clips.PlayerHit);
        }
    }

    private void Start()
    {
        bulletSplitTimer = Random.Range(2, 5);
    }
    void Update()
    {
        bulletBounce();
        bulletSplitTime();
    }


    private void bulletBounce()
    {
        transform.position += dir * Time.deltaTime * speed;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1, LayerMask.GetMask("Wall"));
        dir = dir.normalized;

        if (hit.collider != null)
        {
            if (bulletType == BulletType.NormalBullet)
            {
                dir = Vector3.Reflect(dir, hit.normal);
            }
            else
            {
                bulletSplit();
            }
            bounceCount++;
        }

        if (bounceCount >= bounceMax)
        {
            bounceCount = 0;
            PoolingManager.Instance.RemovePoolingObject(gameObject);
        }
    }


    private void bulletSplitTime()
    {
        if (bulletType == BulletType.SplitBullet)
        {
            timer += Time.deltaTime;
            if (timer >= bulletSplitTimer)
            {
                bulletSplit();
            }
        }
    }
    private void bulletSplit()
    {
        for (int i = 0; i < 8; i++)
        {
            Vector2 dir = dirs[i];
            GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.BossBounceBullet, GameManager.Instance.GetPoolingTemp);
            obj.transform.position = transform.position;

            BounceBullet tempBullet = obj.GetComponent<BounceBullet>();
            tempBullet.SetDir = (dir, 3, 10, 1);
        }
        bounceCount = 0;
        timer = 0;
        bulletSplitTimer = Random.Range(2, 5);
        PoolingManager.Instance.RemovePoolingObject(gameObject);
    }
}
