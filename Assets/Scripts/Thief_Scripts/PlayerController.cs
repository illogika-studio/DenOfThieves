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
        _player.SetCurrentTile(startingTile);
        UpdatePlayerMovement();
    }

    public void UpdatePlayerMovement()
    {
<<<<<<< Updated upstream
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
        _player.UpdateMoveButtonByTile(buttonType, targetTile);
    }

    public void MovePlayer(Tile startingTile)
    {
        _player.SetCurrentTile(startingTile);
        UpdatePlayerMovement();
=======
        /*var currentTileCoordinates = _player.CurrentTile.Coordinates;
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            MovePlayer(new Coordinates(currentTileCoordinates.z, currentTileCoordinates.x - 1));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MovePlayer(new Coordinates(currentTileCoordinates.z, currentTileCoordinates.x + 1));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(new Coordinates(currentTileCoordinates.z - 1, currentTileCoordinates.x));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(new Coordinates(currentTileCoordinates.z + 1, currentTileCoordinates.x));
        }*/
    }

    private void UpdatePlayerMovement(/*Coordinates coordinates*/)
    {
        var currentTileCoordinates = _player.CurrentTile.Coordinates;
        Coordinates upperCoordinates = new Coordinates(currentTileCoordinates.z, currentTileCoordinates.x - 1);
        Coordinates lowerCoordinates = new Coordinates(currentTileCoordinates.z, currentTileCoordinates.x + 1);
        Coordinates leftMostCoordinates = new Coordinates(currentTileCoordinates.z - 1, currentTileCoordinates.x);
        Coordinates rightMostCoordinates = new Coordinates(currentTileCoordinates.z + 1, currentTileCoordinates.x);
        Dictionary<string, Tile> adjacentTiles = new Dictionary<string, Tile>();

        Tile targetTile = _tileManager.GetTileFromCoordinates(upperCoordinates, _player.CurrentRoom.Id);
        adjacentTiles.Add("up", targetTile);
        targetTile = _tileManager.GetTileFromCoordinates(lowerCoordinates, _player.CurrentRoom.Id);
        adjacentTiles.Add("down", targetTile);
        targetTile = _tileManager.GetTileFromCoordinates(leftMostCoordinates, _player.CurrentRoom.Id);
        adjacentTiles.Add("left", targetTile);
        targetTile = _tileManager.GetTileFromCoordinates(rightMostCoordinates, _player.CurrentRoom.Id);
        adjacentTiles.Add("right", targetTile);

        _player.UpdateMovement(adjacentTiles);

        /*if (targetTile is not null)
        {
            _player.SetCurrentTile(targetTile);
        }*/
>>>>>>> Stashed changes
    }

    public void SetPlayer(BasePlayer player)
    {
        _player = player;
    }

<<<<<<< Updated upstream
    public Transform GetTransform()
    {
        return this.transform;
=======
    public enum MoveButton
    {
        Up,
        Down,
        Left,
        Right
>>>>>>> Stashed changes
    }
}
