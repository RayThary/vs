using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvePatten : MonoBehaviour
{
    private List<Vector2> startTrsList = new List<Vector2>();
    private List<Vector2> midTrsList = new List<Vector2>();
    private Vector2 targetTrs;//도착지점

    [SerializeField] private float speed;

    private float value = 0;
    private List<GameObject> bulletObj = new List<GameObject>();
    [SerializeField] private bool initPatten = false;//테스트
    private bool pattenStart = false;

    private Vector3 bezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 P1 = Vector3.Lerp(p0, p1, t);
        Vector3 P2 = Vector3.Lerp(p1, p2, t);

        return Vector3.Lerp(P1, P2, t);
    }

    void Start()
    {
        startTrsList.Clear();
        startTrsList.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)));
        startTrsList.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, -100)));
        startTrsList.Add(Camera.main.ScreenToWorldPoint(new Vector2(-100, Screen.height)));//첫좌표 대각선
        startTrsList.Add(Camera.main.ScreenToWorldPoint(new Vector2(-100, -100)));//첫좌표 오른쪽

        value = 1;
    }

    void Update()
    {
        setPatten();
        bulletMove();

    }

    private void setPatten()
    {
        if (initPatten == true)
        {

            for (int i = 0; i < startTrsList.Count; i++)
            {
                bulletObj.Add(PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.CurveImage, transform));
                bulletObj[i].transform.position = startTrsList[i];
            }
            setPostion();
            initPatten = false;
            pattenStart = true;
        }
    }

    private void setPostion()
    {
        for (int i = 0; i < startTrsList.Count; i++)
        {
            float x;
            float y;
            if (i == 0)
            {
                x = Random.Range(0, startTrsList[0].x);
                y = Random.Range(-startTrsList[0].y, 0);
            }
            else if (i == 1)
            {
                x = Random.Range(-startTrsList[0].x, 0);
                y = Random.Range(-startTrsList[0].y, 0);
            }
            else if (i == 2)
            {
                x = Random.Range(-startTrsList[0].x, 0);
                y = Random.Range(0, startTrsList[0].y);
            }
            else
            {
                x = Random.Range(0, startTrsList[0].x);
                y = Random.Range(0, startTrsList[0].y);
            }
            midTrsList.Add(new Vector2(x, y));
        }
    }

    private void bulletMove()
    {
        if (pattenStart == false)
        {
            return;
        }

        for (int i = 0; i < startTrsList.Count; i++)
        {
            bulletObj[i].transform.position = bezier(startTrsList[i], midTrsList[i], targetTrs, value);
        }
        value -= Time.deltaTime * speed * 0.1f;
        if (value <= 0)
        {
            for (int i = 0; i < startTrsList.Count; i++)
            {
                PoolingManager.Instance.RemovePoolingObject(bulletObj[i]);
            }
            targetTrs = Vector2.zero;
            value = 1;
            pattenStart = false;
        }
    }

    /// <summary>
    /// 패턴시작
    /// </summary>
    /// <param name="_trs">시작위치</param>
    public void SetPattenStart(Vector2 _trs)
    {

        bulletObj.Clear();
        targetTrs = _trs;
        initPatten = true;
    }
}
