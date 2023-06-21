using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTile : Tile
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [HideInInspector] public bool _isOffset;

    protected override void Init()
    {
        base.Init();
    }

    public virtual void SetColor()
    {
        _renderer.color = _isOffset ? _offsetColor : _baseColor;
    }
}
