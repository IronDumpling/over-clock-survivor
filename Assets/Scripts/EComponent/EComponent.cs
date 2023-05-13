using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EComponent : MonoBehaviour
{
   
    public EComponentType type = EComponentType.None;

    public void RunByCurrent()
    {
        
        Debug.Log(transform.name + "被激活");
        ActivateSelf();
       
    }

    protected virtual void ActivateSelf()
    {
        
    }
}
