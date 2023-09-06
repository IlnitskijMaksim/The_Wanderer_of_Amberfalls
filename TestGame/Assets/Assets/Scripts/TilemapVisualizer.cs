using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> floorPositions, Tilemap floorTilemap, TileBase floorTile)
    {
        foreach (var position in floorPositions)
        {
            PaintSingleTile(floorTilemap, floorTile, position);
        }
    }
    internal void PaintBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTop, position);
    }

    private void PaintSingleTile(Tilemap floorTilemap, TileBase floorTile, Vector2Int position)
    {
        var tilePosition = floorTilemap.WorldToCell((Vector3Int)position);
        floorTilemap.SetTile(tilePosition, floorTile);
    }

   public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
