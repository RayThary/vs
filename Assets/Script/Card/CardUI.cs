using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    private IAddon addon;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text description;

    public void Init(IAddon addon)
    {
        this.addon = addon;
        icon.sprite = addon.Sprite;
        description.text = addon.Description;
        gameObject.SetActive(true);
    }
}
