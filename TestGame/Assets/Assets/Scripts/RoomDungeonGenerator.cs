using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField] 
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0,10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = GenerationAlgorithm.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition,
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRooms(roomsList);

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);

        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        throw new NotImplementedException();
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var rooms in roomsList)
        {
            for (int col = offset; col < rooms.size.x - offset; col++)
            {
                for (int row = offset; row < rooms.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)rooms.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
