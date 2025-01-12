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
    public Transform GetTargetTrs { get { return autoTarget.GetTarget; } }

    [SerializeField]
    private Material material;
    public Material Material { get { return material; } }

    [SerializeField]
    private Sprite[] magic;
    public Sprite[] Magic { get => magic; }

    private float timescale = 1;
    public float TimeScale { get { return timescale; } set { Time.timeScale = value; timescale = value; } }

    private bool timeStop = false;
    [SerializeField] private Transform PoolingTemp;
    public Transform GetPoolingTemp { get { return PoolingTemp; } }

    [SerializeField] private mirror m_mirror;
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



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(Time.timeScale);
        }
    }
    private void Awake()
    {
        instance = this;

        player = FindObjectOfType<Player>();
        autoTarget = transform.GetComponent<AutoTarget>();
        Debug.Log(timescale);
    }

    public void SetCharactor(Transform _trs)
    {
        seletCharactor = _trs;
        Transform mirrorTrs = _trs.GetChild(0).transform;
        m_mirror.SetCharacter(_trs, mirrorTrs);
    }

}
