using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private GameObject _tilePrefab;

    private int _scale;
    private Grid<GameObject> _tiles;
    private Vector3 _midPoint;

    private void Awake()
    {
        _scale = (int)_tilePrefab.transform.localScale.x;

        _midPoint = new Vector3(transform.position.x + (float)_width * _scale / 2 - 0.5f,
                                transform.position.y + (float)_height * _scale / 2 - 0.5f);

        _tiles = new Grid<GameObject>(_width, _height, _scale, transform.position,
            createGridObject: (Grid, x, y) =>
            {
                Vector3 position = new Vector3(x * _scale + transform.position.x,
                                               y * _scale + transform.position.y, 1f);
                GameObject spwanedTile = Instantiate(_tilePrefab, position, Quaternion.identity);
                spwanedTile.name = $"Board_{x}_{y}";
                spwanedTile.transform.SetParent(gameObject.transform);
                return spwanedTile;
            });
    }

    private void Start()
    {
        SetCameraFollow();
    }

    private void SetCameraFollow()
    {
        CameraManager.Instance.boardFollowPoint.position = _midPoint;
    }
}
