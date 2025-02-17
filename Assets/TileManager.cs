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
        for (float x = rect.xMin - 2f; x < rect.xMax + 2f; x++)
        {
            for (float y = rect.yMin - 2f; y < rect.yMax + 2f; y++)
            {
                Instantiate(tilePre, new Vector3(x, y, 0), Quaternion.identity, transform);
            }
        }
    }
}
