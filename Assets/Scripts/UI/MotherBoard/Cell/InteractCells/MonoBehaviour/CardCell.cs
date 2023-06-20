using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCell : Cell
{
    public bool CanRun { get; set; }
    private CardCellSO cardCellData;

    private string _title;
    public string m_title
    {
        get => _title;
        private set
        {
            _title = value;
            cardCellData.title = _title;
        }
    }

    protected override void Init()
    {
        base.Init();
        Birth();
    }

    protected virtual void Birth()
    {
        m_title = cardCellData.title;
    }

    public void Execute()
    {
        if (!CanRun) return;
        //DebugLogger.Log(this.name, "is activated");
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
