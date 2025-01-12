using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.TextCore.Text;
using static UnityEngine.UI.Image;

public class mirror : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCam;
    private CinemachineVirtualCamera mirrorCam;
    private Transform charactor;
    private Transform mirrorPlayer;

    private bool camChange = false;

    private bool potalCheck = false;
    private GameObject potalObj;

    private bool timeCheck = false;
    private bool timeSlow = false;
    private float time = 0;

    [SerializeField] private float lineSpeed = 15;
    private bool lineCheck = false;
    private GameObject leftLine;
    private GameObject rightLine;

    private bool camMirrorChange = false;
    void Start()
    {

        mirrorCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            potalObj = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Potal, GameManager.Instance.GetPoolingTemp);
            potalObj.transform.position = charactor.position;
            potalObj.GetComponent<Potal>().PotalOpen = true;
            potalCheck = true;
            timeCheck = true;
            timeSlow = true;
        }

        mirroTime();

        potalAndLine();


    }

    private void mirroTime()
    {
        if (timeCheck)
        {
            if (timeSlow)
            {
                if (time >= 0)
                {
                    time -= Time.unscaledDeltaTime;
                    if (time <= 0)
                    {
                        time = 0f;
                        timeCheck = false;
                        timeSlow = false;
                    }
                    GameManager.Instance.TimeScale = time;
                }
            }
            else
            {
                if (time <= 1)
                {
                    time += Time.unscaledDeltaTime;
                    if (time >= 1)
                    {
                        time = 1;
                        timeCheck = false;
                    }
                    GameManager.Instance.TimeScale = time;
                }
            }
        }
    }

    private void potalAndLine()
    {
        if (potalCheck)
        {
            if (potalObj.transform.localScale.x >= 1)
            {
                leftLine = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.TapLineLeft, GameManager.Instance.GetPoolingTemp);
                rightLine = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.TapLineRight, GameManager.Instance.GetPoolingTemp);
                potalObj.GetComponent<Potal>().PotalClose = true;
                camMirrorChange = true;
                potalCheck = false;
                lineCheck = true;
            }
        }

        if (lineCheck)
        {
            leftLine.transform.position += Vector3.right * lineSpeed * Time.unscaledDeltaTime;
            rightLine.transform.position += Vector3.left * lineSpeed * Time.unscaledDeltaTime;
            if (camMirrorChange)
            {
                if (leftLine.transform.position.x >= Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2)).x)
                {
                    Debug.Log("124");
                    WindowMirror();
                    potalObj.transform.position = charactor.position;
                    potalObj.GetComponent<Potal>().PotalOpen = true;
                    potalObj.GetComponent<Potal>().AutoPotalClose = true;
                    camMirrorChange = false;
                }
            }
            if (leftLine.transform.position.x >= Camera.main.ScreenToWorldPoint(new Vector2(Screen.width + 200, Screen.height / 2)).x)
            {
                timeCheck = true;
                lineCheck = false;
                StartCoroutine(lineRemove());
            }
        }
    }

    IEnumerator lineRemove()
    {
        yield return new WaitForSeconds(0.2f);
        PoolingManager.Instance.RemovePoolingObject(leftLine);
        PoolingManager.Instance.RemovePoolingObject(rightLine);
    }

    private void WindowMirror()
    {
        if (camChange)
        {
            mainCam.Priority = 10;
            mirrorCam.Priority = 9;
            camChange = false;
            charactor.position = mirrorPlayer.position;
        }
        else
        {
            mainCam.Priority = 9;
            mirrorCam.Priority = 10;
            camChange = true;
            charactor.position = mirrorPlayer.position;
        }
    }

    //캐릭터 선택시 이부분 설정 해주어야함
    public void SetCharacter(Transform charTrs, Transform mirrorTrs)
    {
        charactor = charTrs;
        mirrorPlayer = mirrorTrs;

        mainCam.LookAt = charTrs;
        mainCam.Follow = charTrs;

        mirrorCam.LookAt = mirrorTrs;
        mirrorCam.Follow = mirrorTrs;
    }
}
