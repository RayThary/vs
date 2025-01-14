using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOne : Character
{
    private Magic_Axe _Axe;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        _Axe = new Magic_Axe(GameManager.Instance.GetPlayer);
        GameManager.Instance.GetPlayer.Armory.Addon(_Axe);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
