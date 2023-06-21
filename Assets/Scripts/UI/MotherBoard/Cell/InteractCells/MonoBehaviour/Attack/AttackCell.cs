using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCell : CardCell
{
    private void Awake()
    {
        
    }
    
    protected override void ActivateSelf()
    {
        CellCommand c = new CellCommand();
        c.type = CellType.Attack;
        c.multiTimes = 1;
        c.scaleMultiTimes = 1;
        c.dmg = 1;
        CellManager.Instance.AddCommand(c);
    }
}