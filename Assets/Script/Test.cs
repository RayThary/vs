using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float num;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = new Vector3(Mathf.Cos((1 + timer) * Mathf.PI), Mathf.Sin((1 + timer) * Mathf.PI), 0);
    }
}
