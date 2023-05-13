using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportEComponent : EComponent
{
    [SerializeField] private int multiTimes = 2;
    [SerializeField] private float scaleMultiTimes = 1.2f;
    [SerializeField] private int dmg = 3;
    private void Awake()
    {
        type = EComponentType.Support;
    }
    
    protected override void ActivateSelf()
    {
        EComponentCommand c = new EComponentCommand();
        c.type = EComponentType.Support;
        c.multiTimes = multiTimes;
        c.scaleMultiTimes = scaleMultiTimes;
        c.dmg = dmg;
        EComponentManager.Instance.AddCommand(c);
    }
}
