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
    }
    private Magic_9 fire;
    private Magic_15 bounce;
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
            Addon(bounce);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Remove(bounce);
        }
    }
}
