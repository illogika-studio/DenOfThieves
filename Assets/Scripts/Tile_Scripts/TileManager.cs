using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileManager : MonoBehaviour
{
    ILevelManager _levelManager;
    
    [SerializeField] private Dictionary<int, List<Tile>> TilesListPerRooms = new Dictionary<int, List<Tile>>();

    [Inject]
    public void Init(ILevelManager levelManager)
    {
        _levelManager = levelManager;
    }

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
