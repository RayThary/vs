using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] private float gameTime => GameManager.Instance.GameTimer;

    private int level => GameManager.Instance.GetStageLevel;
    [SerializeField]
    private int nextLevel;

    private Transform enemyParent;
    [SerializeField]
    private int enemyCount = 0;
    private int minSpawn = 1;
    public bool test = false;
    public bool spawn = false;
    public bool lastboss = false;
    void Start()
    {
        nextLevel = level + 1;
        enemyParent = transform.Find("EnemyParent");
    }

    void Update()
    {

        if (test)
        {
            if (spawn)
            {

                //monsterSpawn(10);
                GameManager.Instance.SetGameTime = false;
                PoolingManager.Instance.RemoveAllPoolingObject(enemyParent.gameObject);
                GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.BossSpawnEffect, enemyParent);
                obj.GetComponent<BossSpawnEffect>().SetIsLastBoss = lastboss;
                spawn = false;
            }
            return;
        }

        enemyCount = enemyParent.childCount;
        if (enemyCount < 20)
        {
            minSpawn = level;
            spawnEnemy();
        }
        else if (enemyCount <= 300)
        {
            minSpawn = level <= 5 ? 1 : 5;
            spawnEnemy();
        }

        spawnBossEnemy();

    }
    private void spawnEnemy()
    {
        if (gameTime > 0.5f)
        {
            timer += Time.deltaTime;
        }
        if (timer >= (level <= 5 ? 0.5f : 2))
        {

            int spawnCount = Random.Range(level <= 5 ? minSpawn : level + 1, level <= minSpawn ? 3 : level);
            monsterSpawn(spawnCount);
            timer = 0;
        }

    }

    private void monsterSpawn(int _spawnCount)
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject obj;
            PoolingManager.ePoolingObject spawnObject;
            if (level >= 3)
            {
                int spawnType = Random.Range(0, 10);
                if (level >= 6)
                {
                    if (spawnType >= 6)
                        spawnObject = PoolingManager.ePoolingObject.LEnemy;
                    else if (spawnType >= 2)
                        spawnObject = PoolingManager.ePoolingObject.MEnemy;
                    else
                        spawnObject = PoolingManager.ePoolingObject.SEnemy;
                }
                else
                {
                    spawnObject = spawnType >= 6 ? PoolingManager.ePoolingObject.MEnemy : PoolingManager.ePoolingObject.SEnemy;
                }
            }
            else
            {
                spawnObject = PoolingManager.ePoolingObject.SEnemy;
            }
            obj = PoolingManager.Instance.CreateObject(spawnObject, enemyParent);

            Vector2 screenVec = Vector2.zero;
            int objPosiType = Random.Range(0, 4);
            int h = Random.Range(0, Screen.height);
            int w = Random.Range(0, Screen.width);
            switch (objPosiType)
            {
                case 0:
                    screenVec = Camera.main.ScreenToWorldPoint(new Vector2(-100, h));
                    break;
                case 1:
                    screenVec = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width + 100, h));
                    break;
                case 2:
                    screenVec = Camera.main.ScreenToWorldPoint(new Vector2(w, -100));
                    break;
                case 3:
                    screenVec = Camera.main.ScreenToWorldPoint(new Vector2(w, Screen.height + 100));
                    break;
            }

            obj.transform.position = screenVec;
        }
    }

    private void spawnBossEnemy()
    {
        if (level >= nextLevel)
        {
            nextLevel++;


            if (nextLevel != 6 || nextLevel != 11)
            {
                if (nextLevel % 2 == 0)
                {
                    PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.EnemyBoss, enemyParent);
                }
            }
            else
            {
                GameManager.Instance.SetGameTime = false;
                PoolingManager.Instance.RemoveAllPoolingObject(enemyParent.gameObject);
                if (nextLevel == 6)
                {
                    GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.BossSpawnEffect, enemyParent);
                    obj.GetComponent<BossSpawnEffect>().SetIsLastBoss = false;
                }
                else
                {
                    GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.BossSpawnEffect, enemyParent);
                    obj.GetComponent<BossSpawnEffect>().SetIsLastBoss = true;
                }
            }

        }

    }



}
