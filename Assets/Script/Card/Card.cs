using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Card
{
    public void Select();
    public Sprite Sprite { get; }
}

public class AttackDamage_Card : Card
{
    private float damage;
    public Sprite Sprite { get => null; }

    public AttackDamage_Card()
    {
        damage = 5;
    }

    public void Select()
    {
        Debug.Log("a");
        GameManager.Instance.GetPlayer.Stat.AttackDamage += damage;
    }
}