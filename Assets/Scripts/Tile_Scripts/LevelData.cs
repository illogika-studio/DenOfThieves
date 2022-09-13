using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour, ILevelData
{
    [SerializeField]
    private List<Room> _roomsList = new List<Room>();

    public List<Room> RoomsList
    {
        get => _roomsList;
        set => _roomsList = value;
    }
}
