using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TileManager : MonoBehaviour, ITileManager
{
    private ILevelData _levelData;
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
                StartingTilesList.Add(room.StartingTile);   
            }
        }
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
}
