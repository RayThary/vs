using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class mirror : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCam;
    [SerializeField] private CinemachineVirtualCamera mirrorCam;
    public Transform player;
    public Transform mirrorPlayer;

    private bool camChange = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Vector3 original = transform.position;
            if (camChange)
            {
                mainCam.Priority = 10;
                mirrorCam.Priority = 9;
                camChange = false;
                playerChange();
                Vector3 now = transform.position;

                Vector3 delta = original - now;


                mirrorCam.OnTargetObjectWarped(player.transform, delta);
            }
            else
            {
                mainCam.Priority = 9;
                mirrorCam.Priority = 10;
                camChange = true;

                playerChange();
                Vector3 now = transform.position;

                Vector3 delta = original - now ;


                mirrorCam.OnTargetObjectWarped(player.transform, delta);
            }

        }
    }

    private void playerChange()
    {
        Vector2 mid = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));

        Vector2 right = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height / 2));
        Vector2 left = new Vector2(-right.x, right.y);


        if (player.position.x > mid.x)
        {
            float x = mid.x - player.position.x;


            player.transform.position = new Vector2(x, right.y);
        }
        else
        {
            float x = mid.x - player.position.x;
            player.transform.position = new Vector2(x, right.y);
        }




    }
}
