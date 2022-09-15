using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TileManager : MonoBehaviour, ITileManager
{
    private ILevelData _levelData;
    private Room _startingRoom;
    public Room StartingRoom => _startingRoom;
    public Dictionary<int, List<Tile>> TilesListPerRooms { get; } = new Dictionary<int, List<Tile>>();
    public List<Tile> StartingTilesList { get; } = new List<Tile>();

    [Inject]
    public void Init(ILevelData levelData)
    {
        _levelData = levelData;
    }

    private void Start()
    {
        //Populate the list of tiles per rooms and the list of starting tiles
        foreach (var room in _levelData.RoomsList)
        {
            TilesListPerRooms.Add(room.Id, room.TilesList);
            
            if (room.StartingTile != null)
            {
                if(StartingTilesList.Count < 1)
                {
                    _startingRoom = room;
                }

                StartingTilesList.Add(room.StartingTile);   
            }
        }

        foreach (var door in _levelData.DoorsList)
        {
            door.SetupDoor();
        }
    }

    public Room GetRoomFromTile(Tile tile)
    {
        int roomId = GetRoomIdFromTile(tile);
        return _levelData.RoomsList.Find(x => x.Id == roomId);
    }

    public int GetRoomIdFromTile(Tile tile)
    {
        int myKey = TilesListPerRooms.FirstOrDefault(x => x.Value.Contains(tile)).Key;
        return myKey;
    }
    
    public Tile GetTileFromCoordinates(Coordinates coordinates)
    {
        foreach (var room in TilesListPerRooms)
        {
            foreach (var tile in room.Value)
            {
                if (tile.Coordinates == coordinates)
                {
                    return tile;
                }
            }
        }

        return null;
    }

    public Tile GetTileFromCoordinates(Coordinates coordinates, int roomID)
    {
        foreach (var tile in TilesListPerRooms[roomID])
        {
            if (tile.Coordinates == coordinates)
            {
                return tile;
            }
        }
        return null;
    }
}
