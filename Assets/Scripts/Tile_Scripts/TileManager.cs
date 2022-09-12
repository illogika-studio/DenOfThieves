using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileManager : MonoBehaviour
{
    [Inject] private ILevelManager _levelManager;
    
    public Dictionary<int, List<Tile>> TilesListPerRooms = new Dictionary<int, List<Tile>>();

    private void Start()
    {
        if (TilesListPerRooms.Count == 0)
        {
            foreach (var room in _levelManager.RoomsList)
            {
                TilesListPerRooms.Add(room.Id, room.TilesList);
            }
        }
    }
}
