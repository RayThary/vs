using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pa : MonoBehaviour
{
    public bool a = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (a)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }
    }
}
