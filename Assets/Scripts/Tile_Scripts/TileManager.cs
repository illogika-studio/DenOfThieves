using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileManager : MonoBehaviour, ITileManager
{
    private ILevelData _levelData;
    private Dictionary<int, List<Tile>> TilesListPerRooms = new Dictionary<int, List<Tile>>();
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
            if (TilesListPerRooms.Count == 0)
            {
                TilesListPerRooms.Add(room.Id, room.TilesList);
            }
            
            if (room.StartingTile != null)
            {
                StartingTilesList.Add(room.StartingTile);   
            }
        }
    }
}
