using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text description;

    public void Init(IAddon addon)
    {
        icon.sprite = addon.Sprite;
        description.text = addon.Description;
        gameObject.SetActive(true);
    }
}
