using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelData
{
    public List<Room> RoomsList { get; set; }

    public List<Door> DoorsList { get; set; }
}
