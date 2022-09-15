using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileManager
{
    public Dictionary<int, List<Tile>> TilesListPerRooms { get; }
    public Room StartingRoom { get; }
    public List<Tile> StartingTilesList { get; }

    public Room GetRoomFromTile(Tile tile);
    public int GetRoomIdFromTile(Tile tile);
    public Tile GetTileFromCoordinates(Coordinates coordinates);
    public Tile GetTileFromCoordinates(Coordinates coordinates, int roomID);
}
