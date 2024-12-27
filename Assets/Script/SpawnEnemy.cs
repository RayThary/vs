using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject m_enemy;
    public bool tes=false;
    void Start()
    {
        InvokeRepeating("monsterSpawn", 0.1f, 2f);

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            monsterSpawn();
        }

    }

    private void monsterSpawn()
    {
        GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.monster, transform);
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
        if (tes)
        {
            CancelInvoke();
        }
    }
}
