using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileManager
{
    public Dictionary<int, List<Tile>> TilesListPerRooms { get; }
    public List<Tile> StartingTilesList { get; }
    
    public int GetRoomIdFromTile(Tile tile);
    public Tile GetTileFromCoordinates(Coordinates coordinates);
}
