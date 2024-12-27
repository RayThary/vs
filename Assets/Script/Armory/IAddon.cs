using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAddon
{
    public void Update();
    public void Addon();
    public void Remove();
    public void LevelUp();
    public Sprite Sprite { get; }
    public float Statistics { get; set; }
}
