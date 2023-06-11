using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

public class GridCellManager : MonoSingleton<GridCellManager>
{
	[SerializeField] private int _width, _height;
	[SerializeField] private Cell _tilePrefab;
	private Vector3 _midPoint;
    private Dictionary<Vector2, Cell> _tiles;

    private void Awake()
    {
        _midPoint = new Vector3(gameObject.transform.position.x + (float)_width / 2 - 0.5f,
								gameObject.transform.position.y + (float)_height / 2 - 0.5f);
    }

    public void GenerateGrid()
	{
		_tiles = new Dictionary<Vector2, Cell>();

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

		CameraManager.Instance.boardFollowPoint.position = _midPoint;
	}

	public Cell GetTileAtPosition(Vector2 pos)
	{
		if (_tiles.TryGetValue(pos, out var tile)) return tile;
		return null;
	}
}

