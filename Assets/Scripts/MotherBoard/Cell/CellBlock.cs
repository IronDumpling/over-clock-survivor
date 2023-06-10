using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBlock : MonoBehaviour
{
    public bool CanRun { get; set; }
    private void OnEnable()
    {
        CellManager.Instance.RegisterEC(this);
    }

    private void OnDisable()
    {
        CellManager.Instance.DeleteEC(this);
    }


    public void RunBlock()
    {
        if (!CanRun) return;
        this.transform.parent.GetComponent<Cell>().RunByCurrent();
        CanRun = false;
    }
}
