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

		//GenerateTile();

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

	//public void GenerateTile()
	//{
	//	for(int col = 0; col < _width * _scale; col+=_scale)
	//	{
	//		for(int row = 0; row < _height * _scale; row+=_scale)
	//		{
	//			Vector3 position = new Vector3(gameObject.transform.position.x + col,
	//										   gameObject.transform.position.y + row, 1f);

	//			var spwanedTile = Instantiate(_tilePrefab, position, Quaternion.identity);
	//			spwanedTile.name = $"Tile_{col}_{row}";
	//			spwanedTile.transform.SetParent(gameObject.transform);
	//		}
	//	}
	//}
}

