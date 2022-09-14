using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    private Tile _currentTile;
    private Room _currentRoom;
    public Tile CurrentTile => _currentTile;
    public Room CurrentRoom => _currentRoom;

    public void SetCurrentTile(Tile tile)
    {
        _currentTile = tile;
        transform.position = _currentTile.transform.position;
    }

    public void SetCurrentRoom(Room room)
    {
        _currentRoom = room;
    }
}
