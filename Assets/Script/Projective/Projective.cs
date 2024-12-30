using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projective : MonoBehaviour
{
    private List<IP_Attribute> p_Attributes;
    public List<IP_Attribute> Attributes { get { return p_Attributes; } }


    public void Init()
    {
        p_Attributes = new();
    }

    void Update()
    {
        if(p_Attributes != null)
        {
            p_Attributes.ForEach(a => a.Update());
            p_Attributes.ForEach(a => a.LateUpdate());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        p_Attributes.ForEach(x  => x.Enter(collision));
        p_Attributes.ForEach(x => x.LateEnter(collision));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        p_Attributes.ForEach(x => x.Exit(collision));
        p_Attributes.ForEach(x => x.LateExit(collision));
    }
}
