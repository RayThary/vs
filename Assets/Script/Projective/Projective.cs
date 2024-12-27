using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projective : MonoBehaviour
{
    private readonly List<IP_Attribute> p_Attributes = new ();
    public List<IP_Attribute> Attributes { get { return p_Attributes; } }


    void Update()
    {
        p_Attributes.ForEach (a => a.Update ());
        p_Attributes.ForEach(a => a.LateUpdate ());
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
