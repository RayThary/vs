using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_8 : IAddon
{
    public Sprite Sprite => GameManager.Instance.magic[7];

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    public void Addon()
    {

    }

    public void LevelUp()
    {

    }

    public void Remove()
    {

    }

    public void Update()
    {

    }
}
