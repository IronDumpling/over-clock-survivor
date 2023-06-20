using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTile : Tile
{
    [SerializeField] private Color _baseColor, _offsetColor;
    private bool _isOffset;

    protected override void Init()
    {
        base.Init();
        var row = transform.position.x;
        var col = transform.position.y;
        _isOffset = (row % 2 == 0 && col % 2 != 0) || (row % 2 != 0 && col % 2 == 0);
        _renderer.color = _isOffset ? _offsetColor : _baseColor;
    }
}
