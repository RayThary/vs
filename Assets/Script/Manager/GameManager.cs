using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }


    private float playerExpDistance = 3;
    public float GetExpDistance { get { return playerExpDistance; } }
    private Player player;
    public Player GetPlayer { get { return player; } }

    private Transform seletCharactor;
    public Transform GetCharactor { get { return seletCharactor; } }

    private AutoTarget autoTarget;
    public Transform GetTargetTrs { get { if (autoTarget == null) return null; return autoTarget.GetTarget; } }

    [SerializeField]
    private Material material;
    public Material Material { get { return material; } }

    [SerializeField]
    private Sprite[] magic;
    public Sprite[] Magic { get => magic; }


    [SerializeField]
    private Sprite axe;
    public Sprite Axe { get { return axe; } }
    [SerializeField]
    private Sprite arrow;
    public Sprite Arrow { get { return arrow; } }

    [SerializeField]
    private float gamePlayingTimer = 0;
    public float GameTimer { get { return gamePlayingTimer; } set { gamePlayingTimer = value; } }

    private bool gameTime = false;
    public bool SetGameTime { set { gameTime = value; } }
    [SerializeField]
    private int stageLevel = 1;
    public int GetStageLevel { get { return stageLevel; } }

    private int nextLevelTime = 90;

    private float timescale = 1;

    public float TimeScale { get { return timescale; } set { Time.timeScale = value; timescale = value; } }

    private bool timeStop = false;

    [SerializeField] private Transform PoolingTemp;
    public Transform GetPoolingTemp { get { return PoolingTemp; } }

    [SerializeField]
    private Transform enemyParent;
    public Transform GetEnemyPoolingTemp { get { return enemyParent; } }

    [SerializeField]
    private GameObject over;

    [SerializeField]
    private CinemachineImpulseSource shakingWindow;

    [SerializeField]
    private bool isInvincibility;
    public bool IsInvincibility { get => isInvincibility; }

    public bool TimeStop
    {
        get
        {
            return timeStop;
        }
        set
        {
            timeStop = value;
            if (timeStop)
                Time.timeScale = 0;
            else
                Time.timeScale = timescale;
        }
    }


    private void Awake()
    {
        instance = this;

        player = FindObjectOfType<Player>();
        autoTarget = transform.GetComponent<AutoTarget>();

    }

    private void Update()
    {
        gamePlayingTime();
    }

    private void gamePlayingTime()
    {
        if (gameTime)
        {
            gamePlayingTimer += Time.deltaTime * 2;
            if (gamePlayingTimer >= nextLevelTime)
            {
                nextLevelTime += 90;
                stageLevel++;
            }
        }
    }

    public void SetShakingWindow()
    {
        if (player.Setting.Shaking == true)
        {
            shakingWindow.GenerateImpulse();
        }
        //    float nowHp = seletCharactor.GetComponent<Character>().HP;

        //    if (charactorBeforHp > nowHp && charactorBeforHp != 0)
        //    {
        //shakingWindow.GenerateImpulse();
        //    }
        //}
    }

    public void SetCharactor(Transform _trs)
    {
        seletCharactor = _trs;
        Transform mirrorTrs = _trs.GetChild(0).transform;
        gamePlayingTimer = 0;
        gameTime = true;
    }

    public void GameOver()
    {
        gameTime = false;
        gamePlayingTimer = 0;
        player.Armory.Clear();
        PoolingManager.Instance.RemoveAllPoolingObject(GetPoolingTemp.gameObject);
        PoolingManager.Instance.RemoveAllPoolingObject(GetEnemyPoolingTemp.gameObject);
        //옵션 닫기 버튼에 대한 처리
        ButtonManager.Instance.OptionButtonMainMenu();

        over.SetActive(true);
        TimeStop = true;
    }
    public void GameOver2()
    {
        ButtonManager.Instance.OnButtonMainMenu();
        over.SetActive(false);
        TimeStop = false;
    }



    public Rect CalculateWorldSize()
    {
        Rect rect;

        float size = Camera.main.orthographicSize; // 카메라의 Orthographic Size
        float aspectRatio = (float)Screen.width / Screen.height; // 화면 비율

        // 월드 높이와 너비 계산
        float worldHeight = size * 2;
        float worldWidth = worldHeight * aspectRatio;

        rect = new()
        {
            xMin = -worldWidth * 0.5f,
            xMax = worldWidth * 0.5f,
            yMin = -worldHeight * 0.5f,
            yMax = worldHeight * 0.5f
        };
        return rect;
    }
}
