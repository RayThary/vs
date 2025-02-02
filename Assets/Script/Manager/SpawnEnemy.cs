using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject m_enemy;

    public GameObject testboss;
    public bool istest = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
             GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic1, transform);
            obj.transform.position = Vector3.zero;
           // monsterSpawn();
        }
        if (istest)
        {
            GameObject s = Instantiate(testboss);
            s.transform.position = Vector3.zero;
            istest = false;

        }
    }

    private void monsterSpawn()
    {
        for (int i = 0; i < 1; i++)
        {

            GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Enemy, transform);
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
