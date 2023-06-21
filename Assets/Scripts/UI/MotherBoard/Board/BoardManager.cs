using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private GameObject _tilePrefab;

    private Grid<GameObject> _tiles;
    private int _scale;
    private RectTransform _rectTrans;

    private void Awake()
    {
        _scale = (int)_tilePrefab.GetComponent<RectTransform>().rect.width;

        _rectTrans = GetComponent<RectTransform>();

        _tiles = new Grid<GameObject>(_width, _height, _scale, _rectTrans.position,
            createGridObject: (Grid, x, y) =>
            {
                Vector3 position = new Vector3(x * _scale + _rectTrans.position.x,
                                               y * _scale + _rectTrans.position.y);
                GameObject spwanedTile = Instantiate(_tilePrefab, position, Quaternion.identity);
                spwanedTile.name = $"Board_{x}_{y}";
                spwanedTile.transform.SetParent(gameObject.transform);

                BoardCell tile = spwanedTile.GetComponent<BoardCell>();
                tile._isOffset = (x % 2f == 0 && y % 2f != 0) || (x % 2f != 0 && y % 2f == 0);
                tile.SetColor();

                return spwanedTile;
            });
    }
}
