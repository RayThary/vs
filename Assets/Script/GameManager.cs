using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    { get { return instance; } }


    private float playerExpDistance = 3;
    public float GetExpDistance { get { return playerExpDistance; } }
    private Player player;
    public Player GetPlayer { get { return player; } }

    [SerializeField]
    private Material material;
    public Material Material { get { return material; } }

    [SerializeField]
    private Sprite[] magic;
    public Sprite[] Magic { get => magic; }

    private void Awake()
    {
        instance = this;

        player = FindObjectOfType<Player>();
    }
}
