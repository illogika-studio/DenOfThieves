using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LevelManager();
            }

            return _instance;
        }
    }

    private List<Room> _roomsList;
    public List<Room> RoomsList => _roomsList;
    
    public void TestInstance()
    {
        
    }
}
