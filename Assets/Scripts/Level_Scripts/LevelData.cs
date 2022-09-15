using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour, ILevelData
{
    [SerializeField]
    private List<Room> _roomsList = new List<Room>();
    [SerializeField]
    private List<Door> _doorsList = new List<Door>();

    public List<Room> RoomsList
    {
        get => _roomsList;
        set => _roomsList = value;
    }

    public List<Door> DoorsList
    {
        get => _doorsList;
        set => _doorsList = value;
    }
}
