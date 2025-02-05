using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] private float gameTime = 0;

    private int level = 1;
    private float levelUpCycle = 120;


    public GameObject testboss;
    public bool istest = false;

    void Start()
    {
    }

    void Update()
    {
        gameTime = GameManager.Instance.GameTimer;
        if (gameTime > 0.5f)
        {
            timer += Time.deltaTime;
        }

        if (gameTime >= levelUpCycle)
        {
            level++;
            levelUpCycle += 120;
        }

        if (timer >= (level >= 3 ? 0.5f : 1))
        {
            int sapwnMax = level + 2;
            int spawnCount = Random.Range(1, sapwnMax);
            monsterSpawn(spawnCount);
            timer = 0;
        }


        if (istest)
        {
            GameObject s = Instantiate(testboss);
            s.transform.position = Vector3.zero;
            istest = false;

        }
    }


    private void monsterSpawn(int _spawnCount)
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.SEnemy, transform);
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

}
