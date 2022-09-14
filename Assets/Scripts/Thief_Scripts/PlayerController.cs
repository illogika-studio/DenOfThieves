using System;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private BasePlayer _player;
    private ITileManager _tileManager;
    
    [Inject]
    public void Init(ITileManager tileManager)
    {
        _tileManager = tileManager;
    }
    
    private void Start()
    {
        var startingTile = _tileManager.StartingTilesList[0];
       _player.SetCurrentTile(startingTile);
    }

    private void Update()
    {
        var currentTileCoordinates = _player.CurrentTile.Coordinates;
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            MovePlayer(new Coordinates(currentTileCoordinates.x, currentTileCoordinates.z + 1));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MovePlayer(new Coordinates(currentTileCoordinates.x, currentTileCoordinates.z - 1));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(new Coordinates(currentTileCoordinates.x - 1, currentTileCoordinates.z));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(new Coordinates(currentTileCoordinates.x + 1, currentTileCoordinates.z));
        }
    }

    private void MovePlayer(Coordinates coordinates)
    {
        Tile targetTile = _tileManager.GetTileFromCoordinates(coordinates);

        if (targetTile is not null)
        {
            _player.SetCurrentTile(targetTile);
        }
    }

    public void SetPlayer(BasePlayer player)
    {
        _player = player;
    }
}
