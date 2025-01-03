using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class mirror : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCam;
    private CinemachineVirtualCamera mirrorCam;
    public Transform charactor;
    public Transform mirrorPlayer;

    private bool camChange = false;

    private bool test = false;
    void Start()
    {

        mirrorCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetCharactor != null)
        {
            if (test == false)
            {

                charactor = GameManager.Instance.GetCharactor;
                mirrorPlayer = charactor.transform.GetChild(0).transform;

                mainCam.Priority = 10;
                mirrorCam.Priority = 9;

                mainCam.LookAt = charactor;
                mainCam.Follow = charactor;


                mirrorCam.LookAt = mirrorPlayer;
                mirrorCam.Follow = mirrorPlayer;
                test = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Vector3 original = transform.position;
            if (camChange)
            {
                mainCam.Priority = 10;
                mirrorCam.Priority = 9;
                camChange = false;
                charactor.position = mirrorPlayer.position;
                Vector3 now = transform.position;

                Vector3 delta = original - now;


                mirrorCam.OnTargetObjectWarped(charactor.transform, delta);
            }
            else
            {
                mainCam.Priority = 9;
                mirrorCam.Priority = 10;
                camChange = true;

                charactor.position = mirrorPlayer.position;
                Vector3 now = transform.position;

                Vector3 delta = original - now;

                mirrorCam.OnTargetObjectWarped(charactor.transform, delta);
            }

        }
    }

    //ĳ���� ���ý� �̺κ� ���� ���־����
    public void SetCharacter(Transform charTrs, Transform mirrorTrs)
    {
        mainCam.LookAt = charTrs;
        mainCam.Follow = charTrs;

        mirrorCam.LookAt = mirrorTrs;
       mirrorCam.Follow = mirrorTrs;
    }
}
