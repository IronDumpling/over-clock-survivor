using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCell : Cell
{
    public bool CanRun { get; set; }
    public CardCellSO cardCellData;

    protected override void Init()
    {
        base.Init();
        LoadData();
    }

    protected virtual void LoadData()
    {

    }

    public void Execute()
    {
        if (!CanRun) return;
        DebugLogger.Log(this.name, "is activated");
        ActivateSelf();
        CanRun = false;
    }

    protected virtual void ActivateSelf()
    {

    }

    private void OnEnable()
    {
        CellManager.Instance.RegisterCell(this);
    }

    private void OnDisable()
    {
        CellManager.Instance.DeleteCell(this);
    }

}
