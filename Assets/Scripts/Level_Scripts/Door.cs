using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Door : MonoBehaviour
{
    private Coordinates doorCoordinates;
    private Tuple<Tile, Tile> tilesToDoor;
    private bool locked;
    private ITileManager _tileManager;

    [Inject]
    public void Init(ITileManager tileManager)
    {
        _tileManager = tileManager;
    }

    public void SetupDoor()
    {
        var x = Mathf.Round(transform.position.x);
        var z = Mathf.Round(transform.position.z);

        doorCoordinates = new Coordinates(x, z);

        if (transform.rotation.eulerAngles.y == 90)
        {
            SetUpTupleDoors(_tileManager.GetTileFromCoordinates(doorCoordinates), _tileManager.GetTileFromCoordinates(new Coordinates(x, z - 1)));
        } 
        else if (transform.rotation.eulerAngles.y == 180)
        {
            SetUpTupleDoors(_tileManager.GetTileFromCoordinates(new Coordinates(x - 1, z - 1)), _tileManager.GetTileFromCoordinates(new Coordinates(x, z - 1)));
        }
        else if (transform.rotation.eulerAngles.y == 270)
        {
            SetUpTupleDoors(_tileManager.GetTileFromCoordinates(new Coordinates(x - 1, z - 1)), _tileManager.GetTileFromCoordinates(new Coordinates(x - 1, z)));
        }
        else if (transform.rotation.eulerAngles.y == 0)
        {
            SetUpTupleDoors(_tileManager.GetTileFromCoordinates(doorCoordinates), _tileManager.GetTileFromCoordinates(new Coordinates(x - 1, z)));
        }
    }

    private void SetUpTupleDoors(Tile tileA, Tile tileB)
    {
        if(tileA is not null && tileB is not null)
        {
            tilesToDoor = Tuple.Create(tileA, tileB);
            tileA.AddDoor(this);
            tileB.AddDoor(this);
        }
    }

    public Tile GetOtherSideTile(Tile targetTile)
    {
        if(tilesToDoor is not null)
        {
            if(tilesToDoor.Item1 == targetTile)
            {
                return tilesToDoor.Item2;
            }
            else if (tilesToDoor.Item2 == targetTile)
            {
                return tilesToDoor.Item1;
            }
        }
        return null;
    }
}
