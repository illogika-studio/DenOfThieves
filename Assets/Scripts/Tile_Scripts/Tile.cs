using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public abstract int LightValue { get; }
    public abstract int SoundValue { get; }
    
    private List<Door> doorsList = new List<Door>();

    private int _roomId;
    public int RoomId => _roomId;
    
    public Coordinates Coordinates { get; private set; }
    public Dictionary<InteractableElement, List<Action>> AvailableActionList { get; private set; }
    
    public abstract void UpdateLightValue(int amount);
    public abstract void UpdateSoundValue(int amount);
   
    private void Awake()
    {
        Vector3 v = MathTools.RoundVector3(transform.position);
        transform.position = v;
        
        var offset = GetOffset();
        Coordinates = new Coordinates(v.x + offset.x, v.z + offset.z);
    }
    
    private Vector3 GetOffset()
    {
        Vector3 offset = Vector3.zero;
        
        if (transform.rotation.eulerAngles.y == 90 || transform.rotation.eulerAngles.y == -270)
        {
            offset = new Vector3(0, 0, -1);
        }
        else if (transform.rotation.eulerAngles.y == 180 || transform.rotation.eulerAngles.y == -180)
        {
            offset = new Vector3(-1, 0, -1);
        }
        else if (transform.rotation.eulerAngles.y == 270 || transform.rotation.eulerAngles.y == -90)
        {
            offset = new Vector3(-1, 0, 0);
        }

        return offset;
    }

    public void SetRoomId(int id)
    {
        _roomId = id;
    }
    
    public List<Tile> GetTilesThroughDoors()
    {
        List<Tile> adjacentTiles = new List<Tile>();

        foreach(Door door in doorsList)
        {
            //TODO: Check if door is locked.
            adjacentTiles.Add(door.GetOtherSideTile(this));
        }
        
        return adjacentTiles;
    }

    public void AddDoor(Door door)
    {
        doorsList.Add(door);
    }
}
