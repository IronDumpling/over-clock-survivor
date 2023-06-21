using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Cell : MonoBehaviour
{
    protected Image _img;
    protected RectTransform _rectTrans;
    protected CellType m_type = CellType.None;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _img = GetComponent<Image>();
        _rectTrans = GetComponent<RectTransform>();
    }
}
