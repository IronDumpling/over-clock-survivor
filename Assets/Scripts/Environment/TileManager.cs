using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
	[SerializeField] private int _width, _height;
	[SerializeField] private Tile _tilePrefab;
	
	private int _widthScale, _heightScale;
	private Grid<Tile> _tiles;

    private void Awake()
    {
		_widthScale = (int)_tilePrefab.transform.localScale.x;
		_heightScale = (int)_tilePrefab.transform.localScale.y;

		GenerateTile();

		//_tiles = new Grid<Tile>(_width, _height, _widthScale, transform.position, createGridObject: (Tile, x, y) =>
		//{
		//	return Tile();
		//});
	}

    public void GenerateTile()
	{
		for(int col = 0; col < _width * _widthScale; col+=_widthScale)
		{
			for(int row = 0; row < _height * _heightScale; row+=_heightScale)
			{
				Vector3 position = new Vector3(gameObject.transform.position.x + col,
											   gameObject.transform.position.y + row, 1f);

                var spwanedTile = Instantiate(_tilePrefab, position, Quaternion.identity);
				spwanedTile.name = $"Tile_{col}_{row}";
				spwanedTile.transform.SetParent(gameObject.transform);
            }
		}
	}
}

