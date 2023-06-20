using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
	[SerializeField] private int _width, _height;
	[SerializeField] private GameObject _tilePrefab;
	
	private int _scale;
	private Grid<GameObject> _tiles;

	private void Awake()
	{
		_scale = (int)_tilePrefab.transform.localScale.x;

		_tiles = new Grid<GameObject>(_width, _height, _scale, transform.position,
			createGridObject: (Grid, x, y) =>
		{
            Vector3 position = new Vector3(x * _scale + transform.position.x,
										   y * _scale + transform.position.y, 1f);
			GameObject spwanedTile = Instantiate(_tilePrefab, position, Quaternion.identity);
            spwanedTile.name = $"Tile_{x}_{y}";
            spwanedTile.transform.SetParent(gameObject.transform);
			return spwanedTile;
		});
	}
}

