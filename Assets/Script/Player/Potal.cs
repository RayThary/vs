using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{

    private bool potalOpen = false;
    public bool PotalOpen { get { return potalOpen; } set { potalOpen = value; } }

    private bool potalClose = false;
    public bool PotalClose { get { return potalClose; } set { potalClose = value; } }

    private bool autoPotalClose = false;
    public bool AutoPotalClose { set { autoPotalClose = value; } }

    void Start()
    {

    }


    void Update()
    {

        if (potalOpen)
        {
            if (transform.localScale.x < 1)
            {
                transform.localScale += new Vector3(1, 1) * Time.unscaledDeltaTime * 2;
            }
            else
            {
                potalOpen = false;
                if (autoPotalClose)
                {
                    potalClose = true;
                    autoPotalClose = false;
                }
            }
        }

        if (potalClose)
        {
            if (transform.localScale.x >= 0)
            {
                transform.localScale -= new Vector3(1, 1) * Time.unscaledDeltaTime * 2;
            }
            else
            {
                potalClose = false;
            }
        }
    }

    public void PotalRemove()
    {
        PoolingManager.Instance.RemovePoolingObject(gameObject);
    }
}
