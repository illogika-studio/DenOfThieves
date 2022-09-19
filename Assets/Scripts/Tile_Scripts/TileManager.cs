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
        foreach (var room in _levelData.RoomsList)
        {
            if (room.StartingTile is not null)
            {
                StartingTilesList.Add(room.StartingTile);
            }

            var tileList = new List<Tile>();
            foreach (var tile in room.TilesList)
            {
                tile.SetRoomId(room.Id);
                tileList.Add(tile);
            }
            
            TilesListPerRooms[room.Id] = tileList;
        }
        
        foreach (var door in _levelData.DoorsList)
        {
            door.SetupDoor();
        }
    }

    public Room GetRoomFromTile(Tile tile)
    {
        return _levelData.RoomsList.Find(x => x.Id == tile.RoomId);
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
