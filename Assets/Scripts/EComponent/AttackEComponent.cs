using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEComponent : EComponent
{
    private void Awake()
    {
        type = EComponentType.Attack;
    }
    
    protected override void ActivateSelf()
    {
        EComponentCommand c = new EComponentCommand();
        c.type = EComponentType.Attack;
        c.multiTimes = 1;
        c.scaleMultiTimes = 1;
        c.dmg = 1;
        EComponentManager.Instance.AddCommand(c);
    }
}