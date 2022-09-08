using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private static TileManager _instance;

    public static TileManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TileManager();
            }

            return _instance;
        }
    }
    
    public Dictionary<int, List<Tile>> TilesListPerRooms;

    private void Awake()
    {
        if (TilesListPerRooms.Count == 0)
        {
            foreach (var room in LevelManager.Instance.RoomsList)
            {
                TilesListPerRooms.Add(room.Id, room.TilesList);
            }
        }
    }
}
