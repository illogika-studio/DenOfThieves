using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileManager : MonoBehaviour
{
    ILevelData _levelData;
    
    private Dictionary<int, List<Tile>> TilesListPerRooms = new Dictionary<int, List<Tile>>();

    [Inject]
    public void Init(ILevelData levelData)
    {
        _levelData = levelData;
    }

    private void Start()
    {
        if (TilesListPerRooms.Count == 0)
        {
            foreach (var room in _levelData.RoomsList)
            {
                TilesListPerRooms.Add(room.Id, room.TilesList);
            }
        }
    }
}
