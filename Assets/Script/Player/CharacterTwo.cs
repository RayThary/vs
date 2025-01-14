using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTwo : Character
{
    private Magic_Boom _Boom;
    new void Start()
    {
        base.Start();
        _Boom = new Magic_Boom(GameManager.Instance.GetPlayer);
        GameManager.Instance.GetPlayer.Armory.Addon(_Boom);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
