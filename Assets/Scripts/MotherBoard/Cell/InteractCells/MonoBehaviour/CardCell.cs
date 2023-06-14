using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCell : Cell
{
    public bool CanRun { get; set; }
    private void OnEnable()
    {
        CellManager.Instance.RegisterCell(this);
    }

    private void OnDisable()
    {
        CellManager.Instance.DeleteCell(this);
    }

    public void RunBlock()
    {
        if (!CanRun) return;
        Execute();
        CanRun = false;
    }

    public void Execute()
    {
        Debug.Log(transform.name + "被激活");
        ActivateSelf();
    }

    protected virtual void ActivateSelf()
    {

    }
}
