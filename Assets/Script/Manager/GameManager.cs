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
    public float GameTimer { get { return gamePlayingTimer; } }

    private bool gameTime = false;
    public bool SetGameTime { set { gameTime = value; } }

    private int stageLevel = 1;
    public int GetStageLevel {  get { return stageLevel; } }

    private int nextLevelTime = 90;

    private float timescale = 1;

    public float TimeScale { get { return timescale; } set { Time.timeScale = value; timescale = value; } }

    private bool timeStop = false;
    [SerializeField] private Transform PoolingTemp;
    public Transform GetPoolingTemp { get { return PoolingTemp; } }

    [SerializeField] private mirror m_mirror;
    private int enemyCount = 0;
    public int SetEnemyCount { set { enemyCount = value; } }
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
            gamePlayingTimer += Time.deltaTime;
            if (gamePlayingTimer >= nextLevelTime)
            {
                nextLevelTime += 90;
                stageLevel++;
            }
        }
    }

    public void SetCharactor(Transform _trs)
    {
        seletCharactor = _trs;
        Transform mirrorTrs = _trs.GetChild(0).transform;
        m_mirror.SetCharacter(_trs, mirrorTrs);
        gameTime = true;
    }





    public Rect CalculateWorldSize()
    {
        Rect rect;

        float size = Camera.main.orthographicSize; // ī�޶��� Orthographic Size
        float aspectRatio = (float)Screen.width / Screen.height; // ȭ�� ����

        // ���� ���̿� �ʺ� ���
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
