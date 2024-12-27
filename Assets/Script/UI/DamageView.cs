using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DamageView : MonoBehaviour
{
    private Armory armory;
    private IAddon addon;
    public IAddon Addon => addon;

    [SerializeField]
    private Image image;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Image fill;

    // Update is called once per frame
    void Update()
    {
        if(addon != null && armory != null)
        {
            text.text = addon.Statistics.ToString();
            float sum = armory.Addons.Sum(x => x.Statistics);
            if (sum != 0 && addon.Statistics != 0)
                fill.fillAmount = addon.Statistics / sum;
            else
                fill.fillAmount = 0;
        }
    }


    public void Interlock(Armory armory, IAddon addon)
    {
        this.armory = armory;
        this.addon = addon;
        image.sprite = addon.Sprite;
        text.text = addon.Statistics.ToString();
        float sum = armory.Addons.Sum(x => x.Statistics);
        if (sum != 0 && addon.Statistics != 0)
            fill.fillAmount = addon.Statistics / sum;
        else
            fill.fillAmount = 0;
    }
}
