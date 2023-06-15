using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
	[SerializeField] private int _width, _height;
	[SerializeField] private Grid _tilePrefab;
	private Vector3 _midPoint;
    private Dictionary<Vector2, Grid> _tiles;
	public bool isMotherBoard = true;

    private void Awake()
    {
        _midPoint = new Vector3(gameObject.transform.position.x + (float)_width / 2 - 0.5f,
								gameObject.transform.position.y + (float)_height / 2 - 0.5f);
		GenerateGrid();
	}

    private void Start()
    {
        if (isMotherBoard) SetCameraFollow();
    }

    public void GenerateGrid()
	{
		_tiles = new Dictionary<Vector2, Grid>();

		for(int col = 0; col < _width; col++)
		{
			for(int row = 0; row < _height; row++)
			{
				Vector3 position = new Vector3(gameObject.transform.position.x + col,
											   gameObject.transform.position.y + row, 1f);

                var spwanedTile = Instantiate(_tilePrefab, position, Quaternion.identity);
				spwanedTile.name = $"Tile_{col}_{row}";
				spwanedTile.transform.SetParent(gameObject.transform);

				_tiles[new Vector2(col, row)] = spwanedTile;
            }
		}
	}

	public Grid GetTileAtPosition(Vector2 pos)
	{
		if (_tiles.TryGetValue(pos, out var tile)) return tile;
		return null;
	}

	private void SetCameraFollow()
	{
        CameraManager.Instance.boardFollowPoint.position = _midPoint;
    }
}

