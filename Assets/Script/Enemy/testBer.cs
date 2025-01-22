using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBer : MonoBehaviour
{

    public GameObject a;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject obj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.CurvePatten, transform);
            obj.GetComponent<CurvePatten>().SetPattenStart(a.transform.position);
        }

    }
}
