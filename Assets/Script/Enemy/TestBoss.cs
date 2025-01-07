using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : Enemy
{
    //private Armory armory;
    private PatternTest test;
    public bool right;
    public bool left;
    public bool top;
    public bool bottom;

    protected new void Start()
    {
        base.Start();
        test = new PatternTest();
    }

    protected new void Update()
    {
        base.Update();
        if(left)
        {
            test.Left();
            left = false;
        }
        else if(right)
        {
            test.Right();
            right = false;
        }
        if (top)
        {
            test.Top();
            top = false;
        }
        else if (bottom)
        {
            test.Bottom();
            bottom = false;
        }
    }
}
