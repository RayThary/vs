using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armory : MonoBehaviour
{
    private readonly List<IAddon> addonList = new();
    public List<IAddon> Addons { get { return addonList; } }
    private Player player;
    private Action<IAddon> addCall = null;
    public Action<IAddon> AddCall { get { return addCall; } set { addCall = value; } }
    private Action<IAddon> removeCall = null;
    public Action<IAddon> RemoveCall { get { return removeCall; } set {  removeCall = value; } }

    //Addon추가
    public void Addon(IAddon addon)
    {
        if(addonList.Contains(addon))
        {
            addon.LevelUp();
        }
        else
        {
            addon.Addon();
            addonList.Add(addon);
            addCall?.Invoke(addon);
        }
    }
    //제거
    public void Remove(IAddon addon) 
    {
        addon.Remove();
        addonList.Remove(addon);
        removeCall?.Invoke(addon);
    }
    //초기화
    public void Clear()
    {
        for(int i = 0; i < addonList.Count; i++)
        {
            removeCall?.Invoke(addonList[i]);
        }
        addonList.ForEach(x => x.Remove());
        addonList.Clear();
    }

    private void Start()
    {
        player = GetComponent<Player>();
        Debug.Log("테스트코드");
        fire = new Magic_9(player, 5, 10f, 1, 1);
        magic_15 = new Magic_15(player, 3, 1);
        magic_8 = new Magic_8(player, 3, 1);
        magic_6 = new Magic_6(player, 1);
        magic_7 = new Magic_7(player);
    }
    private Magic_9 fire;
    private Magic_15 magic_15;
    private Magic_8 magic_8;
    private Magic_6 magic_6;
    private Magic_7 magic_7;
    private void Update()
    {
        addonList.ForEach (x => x.Update());
        //테스트부분
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Addon(fire);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Remove(fire);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Addon(magic_15);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Remove(magic_15);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Addon(magic_8);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Remove(magic_8);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Addon(magic_6);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Remove(magic_6);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Addon(magic_7);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Remove(magic_7);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            player.AddExp(1);
        }
    }
}
