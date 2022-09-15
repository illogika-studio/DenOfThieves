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
