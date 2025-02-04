using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterThree : Character
{
    private Magic_Bullet arrow;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        arrow = new Magic_Bullet(GameManager.Instance.GetPlayer);
        GameManager.Instance.GetPlayer.Armory.Addon(arrow);
        GameManager.Instance.GetPlayer.CardSelect.Addons.Add(arrow);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    protected override void Skill()
    {

    }
}
