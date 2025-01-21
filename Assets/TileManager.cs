using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private Transform tilePre;
    [SerializeField]
    private Transform wallPre;

    // Start is called before the first frame update
    void Start()
    {
        //화면을 타일로 꽉 채우기
        Rect rect = GameManager.Instance.CalculateWorldSize();
        for(float x = rect.xMin + 0.5f; x < rect.xMax; x++)
        {
            for (float y = rect.yMin + 0.5f; y < rect.yMax; y++)
            {
                Instantiate(tilePre, new Vector3(x,y,0), Quaternion.identity, transform);
            }
        }
    }
}
