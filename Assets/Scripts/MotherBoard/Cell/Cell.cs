using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell : MonoBehaviour
{
   
    public CellType type = CellType.None;
    [SerializeField] protected SpriteRenderer _renderer;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void RunByCurrent()
    {
        Debug.Log(transform.name + "被激活");
        ActivateSelf();
    }

    protected virtual void ActivateSelf()
    {
        
    }
}
