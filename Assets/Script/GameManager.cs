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

    //임시 테스트용
    [SerializeField] private Transform seletCharactor;
    public Transform GetCharactor { get { return seletCharactor; } }

    private AutoTarget autoTarget;
    public Transform GetTargetTrs { get { return autoTarget.GetTarget; } }

    public Sprite[] magic;

    private void Awake()
    {
        instance = this;

        player = FindObjectOfType<Player>();
        autoTarget = transform.GetComponent<AutoTarget>();

    }

    public void SetCharactor(Transform _trs)
    {
        seletCharactor = _trs;
    }
}
