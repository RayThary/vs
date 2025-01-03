using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectiveCollider : MonoBehaviour
{
    [SerializeField]
    private Projective projective;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(projective.Attributes != null)
        {
            projective.Attributes.ForEach(x => x.Enter(collision));
            projective.Attributes.ForEach(x => x.LateEnter(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (projective.Attributes != null)
        {
            projective.Attributes.ForEach(x => x.Exit(collision));
            projective.Attributes.ForEach(x => x.LateExit(collision));
        }
    }
}
