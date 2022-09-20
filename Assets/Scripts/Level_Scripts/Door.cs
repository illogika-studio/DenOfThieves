using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Door : MonoBehaviour
{
    private Coordinates _doorCoordinates;
    private Tuple<Tile, Tile> _tilesToDoor;
    private ITileManager _tileManager;

    [Inject]
    public void Init(ITileManager tileManager)
    {
        _tileManager = tileManager;
    }

    public void SetupDoor()
    {
        var pos = UtilsClass.RoundVector3(transform.position);

        _doorCoordinates = new Coordinates(pos.x, pos.z);

        Coordinates coordinatesTile1 = new Coordinates();
        Coordinates coordinatesTile2 = new Coordinates();
        
        if (transform.rotation.eulerAngles.y == 90 || transform.rotation.eulerAngles.y == -270)
        {
            coordinatesTile1 = _doorCoordinates;
            coordinatesTile2 = new Coordinates(pos.x, pos.z - 1);
        } 
        else if (transform.rotation.eulerAngles.y == 180 || transform.rotation.eulerAngles.y == -180)
        {
            coordinatesTile1 = new Coordinates(pos.x - 1, pos.z - 1);
            coordinatesTile2 = new Coordinates(pos.x, pos.z - 1);
        }
        else if (transform.rotation.eulerAngles.y == 270 || transform.rotation.eulerAngles.y == -90)
        {
            coordinatesTile1 = new Coordinates(pos.x - 1, pos.z - 1);
            coordinatesTile2 = new Coordinates(pos.x - 1, pos.z);
        }
        else if (transform.rotation.eulerAngles.y == 0)
        {
            coordinatesTile1 = _doorCoordinates;
            coordinatesTile2 = new Coordinates(pos.x - 1, pos.z);
        }
        
        SetTupleDoors(_tileManager.GetTileFromCoordinates(coordinatesTile1), _tileManager.GetTileFromCoordinates(coordinatesTile2));
    }

    private void SetTupleDoors(Tile tile1, Tile tile2)
    {
        if(tile1 is not null && tile2 is not null)
        {
            _tilesToDoor = Tuple.Create(tile1, tile2);
            tile1.AddDoor(this);
            tile2.AddDoor(this);
        }
    }

    public Tile GetOtherSideTile(Tile targetTile)
    {
        if(_tilesToDoor is not null)
        {
            if(_tilesToDoor.Item1 == targetTile)
            {
                return _tilesToDoor.Item2;
            }
            
            if (_tilesToDoor.Item2 == targetTile)
            {
                return _tilesToDoor.Item1;
            }
        }
        
        return null;
    }
}
