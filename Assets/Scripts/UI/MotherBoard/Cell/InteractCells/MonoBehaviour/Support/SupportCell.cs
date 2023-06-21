using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportCell : CardCell
{
    [SerializeField] private int multiTimes = 2;
    [SerializeField] private float scaleMultiTimes = 1.2f;
    [SerializeField] private int dmg = 3;

    private void Awake()
    {
        
    }
    
    protected override void ActivateSelf()
    {
        CellCommand c = new CellCommand();
        c.type = CellType.Support;
        c.multiTimes = multiTimes;
        c.scaleMultiTimes = scaleMultiTimes;
        c.dmg = dmg;
        CellManager.Instance.AddCommand(c);
    }
}
