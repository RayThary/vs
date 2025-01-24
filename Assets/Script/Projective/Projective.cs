using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projective : MonoBehaviour
{
    private List<IP_Attribute> p_Attributes;
    public List<IP_Attribute> Attributes { get { return p_Attributes; } }
    private List<Collider2D> enter;
    private List<Collider2D> exit;

    public void Init()
    {
        p_Attributes = new();
        enter = new();
        exit = new();
    }

    void Update()
    {
        if(p_Attributes != null)
        {
            p_Attributes.ForEach(a => a.Update());
        }
    }

    private void LateUpdate()
    {
        if (p_Attributes != null)
        {
            p_Attributes.ForEach(a => a.LateUpdate());
        }
        if (enter != null && p_Attributes != null)
        {
            enter.ForEach(collider => p_Attributes.ForEach(x => x.LateEnter(collider)));
            enter.Clear();
        }
        if (exit != null && p_Attributes != null)
        {
            exit.ForEach(collider => p_Attributes.ForEach(x => x.LateExit(collider)));
            exit.Clear();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (p_Attributes != null)
        {
            p_Attributes.ForEach(x => x.Enter(collision));
            enter.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (p_Attributes != null)
        {
            p_Attributes.ForEach(x => x.Exit(collision));
            exit.Add(collision);
        }
    }
}
