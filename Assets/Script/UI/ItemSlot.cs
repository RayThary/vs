using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private Guide guide;
    private IAddon addon;
    public IAddon Addon { get { return addon; }}
    [SerializeField]
    private Image icon;
    //public Sprite Sprite { set { icon.sprite = value; } }

    public void Init(Guide guide, IAddon addon)
    {
        this.guide = guide;
        this.addon = addon;
        icon.sprite = addon.Sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        guide.DescriptionON(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        guide.DescriptionOFF(this);
    }
}
