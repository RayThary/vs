using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class missingDestroy : MonoBehaviour
{
    public GameObject Unit;

    [SerializeField]private Transform[] _prefabCount = new Transform[] { };
    public bool DestroyStart = false;



    private void Update()
    {
        if (DestroyStart)
        {
            _prefabCount = Unit.transform.GetComponentsInChildren<Transform>();
            for (int i = 0; i < _prefabCount.Length; i++)
            {
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(_prefabCount[i].gameObject);
            }

            DestroyStart = false;
            Debug.Log("삭제완료");
        }
    }

}
