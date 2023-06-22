using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCell : Cell
{
    [SerializeField] private CardCellSO cardCellData;

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

    private CellType _type;
    public new CellType m_type
    {
        get => _type;
        private set
        {
            _type = value;
            cardCellData.type = _type;
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
        m_type = cardCellData.type;
    }

    public void Execute()
    {
        DebugLogger.Log(this.name, "is activated");
        ActivateSelf();
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
