using System;
using UnityEngine;
public class TileRange
{
    private ITile[] _tiles;
    private ITile _minTile;
    private ITile _maxTile;

    public TileRange(ITile[] tiles)
    {
        _tiles = tiles;
        FillingClass(tiles);
    }

    private void FillingClass(ITile[] tiles)
    {
        _minTile = tiles[0];
        _maxTile = tiles[0];

        foreach (var tile in tiles)
        {
            if (tile.GetPosition().x < _minTile.GetPosition().x && tile.GetPosition().z < _minTile.GetPosition().z)
                _minTile = tile;
            if (tile.GetPosition().z > _maxTile.GetPosition().z && tile.GetPosition().x > _maxTile.GetPosition().x)
                _maxTile = tile;
        }
    }
}

