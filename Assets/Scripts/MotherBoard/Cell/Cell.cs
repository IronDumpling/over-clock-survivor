using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour
{
   
    public CellType type = CellType.None;

    public void RunByCurrent()
    {
        
        Debug.Log(transform.name + "被激活");
        ActivateSelf();
       
    }

    protected virtual void ActivateSelf()
    {
        
    }
}
