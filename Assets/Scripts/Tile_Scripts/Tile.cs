using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public abstract int LightValue { get; }
    public abstract int SoundValue { get; }
    public Coordinates Coordinates { get; private set; }
    public Dictionary<InteractableElement, List<Action>> AvailableActionList { get; private set; }
    public List<Door> doors = new List<Door>();

    public abstract void UpdateLightValue(int amount);
    public abstract void UpdateSoundValue(int amount);
    private void Awake()
    {
        var x = Mathf.Round(transform.position.x);
        var z = Mathf.Round(transform.position.z);

        var offset = GetOffset();

        Coordinates = new Coordinates(x + offset.x, z + offset.z);
    }
    
    public List<Tile> GetTilesThroughDoors()
    {
        List<Tile> adjacentTiles = new List<Tile>();

        foreach(Door door in doors)
        {
            //TODO: Check if door is locked.
            adjacentTiles.Add(door.GetOtherSideTile(this));
        }
        return adjacentTiles;
    }

    public void AddDoor(Door door)
    {
        doors.Add(door);
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

    public Vector3 GetRealPosition()
    {
        return GetRoundedPosition() + GetOffset();
    }

    private Vector3 GetRoundedPosition()
    {
        var roundX = Mathf.Round(transform.position.x);
        var roundY = Mathf.Round(transform.position.y);
        var roundZ = Mathf.Round(transform.position.z);

        return new Vector3(roundX, roundY, roundZ);
    }
}
