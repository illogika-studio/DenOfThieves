using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerController : MonoBehaviour, IPlayerController
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
        _player.SetCurrentRoom(_tileManager.StartingRoom);
        _player.SetCurrentTile(startingTile, instant: true);
        UpdatePlayerMovement();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (_player.CurrentTile is not null)
        {
            var currentTileCoordinates = _player.CurrentTile.Coordinates;

            if (Input.GetKeyDown(KeyCode.D))
            {
                KeyMovement(new Coordinates(currentTileCoordinates.x, currentTileCoordinates.z - 1));
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                KeyMovement(new Coordinates(currentTileCoordinates.x, currentTileCoordinates.z + 1));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                KeyMovement(new Coordinates(currentTileCoordinates.x - 1, currentTileCoordinates.z));
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                KeyMovement(new Coordinates(currentTileCoordinates.x + 1, currentTileCoordinates.z));
            }
        }
#endif
    }
#if UNITY_EDITOR
    private void KeyMovement(Coordinates coordinates)
    {
        Tile targetTile = _tileManager.GetTileFromCoordinates(coordinates, _player.CurrentRoom.Id);

        if(targetTile is null)
        {
            foreach (Tile tile in _player.CurrentTile.GetTilesThroughDoors())
            {
                if (tile.Coordinates == coordinates)
                {
                    targetTile = tile;
                }
            }
        }

        MovePlayer(targetTile);
    }
#endif
    public void UpdatePlayerMovement()
    {
        var currentTileCoordinates = _player.CurrentTile.Coordinates;
        Coordinates rightMostCoordinates = new Coordinates(currentTileCoordinates.x, currentTileCoordinates.z + 1);
        Coordinates leftMostCoordinates = new Coordinates(currentTileCoordinates.x, currentTileCoordinates.z - 1);
        Coordinates lowerCoordinates = new Coordinates(currentTileCoordinates.x + 1, currentTileCoordinates.z);
        Coordinates upperCoordinates = new Coordinates(currentTileCoordinates.x - 1, currentTileCoordinates.z);

        UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType.Up, upperCoordinates);
        UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType.Down, lowerCoordinates);
        UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType.Left, leftMostCoordinates);
        UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType.Right, rightMostCoordinates);
    }

    private void UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType buttonType, Coordinates targetCoordinates)
    {
        Tile targetTile = _tileManager.GetTileFromCoordinates(targetCoordinates, _player.CurrentRoom.Id);
        
        if(targetTile is null)
        {
            foreach(Tile tile in _player.CurrentTile.GetTilesThroughDoors())
            {
                if(tile.Coordinates == targetCoordinates)
                {
                    targetTile = tile;
                }
            }
        }

        _player.UpdateMoveButtonByTile(buttonType, targetTile);
    }

    public void MovePlayer(Tile startingTile)
    {   
        if (startingTile is not null)
        {
            _player.SetCurrentTile(startingTile);
            UpdatePlayerMovement();
        }
    }

    public void SetPlayer(BasePlayer player)
    {
        _player = player;
    }

    public Transform GetTransform()
    {
        return this.transform;
    }
}
