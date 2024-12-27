using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class missingDestroy : MonoBehaviour
{
    public GameObject Unit;

    [SerializeField]private Transform[] _prefabCount = new Transform[] { };


    void Start()
    {

        _prefabCount = Unit.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < _prefabCount.Length; i++)
        {
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(_prefabCount[i].gameObject);
        }

       
    }

}
