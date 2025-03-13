using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BossSpawnEffect : MonoBehaviour
{
    
    private bool isLastBoss = false;
    public bool SetIsLastBoss { set { isLastBoss = value; } }
    [SerializeField]
    private float spawnTime = 2;

    private ParticleSystem particle;
    private float timer = 0.0f;
    private bool loopOut = false;
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            if (!loopOut)
            {
                var pMain = particle.main;
                pMain.loop = false;
                loopOut = true;
                spawnBoss();
            }
        }
    }

    private void spawnBoss()
    {
        if (isLastBoss)
        {
            PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.EnemyLastBoss, GameManager.Instance.GetEnemyPoolingTemp);
        }
        else
        {
            PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.EnemyMiddleBoss, GameManager.Instance.GetEnemyPoolingTemp);
        }
    }
}
