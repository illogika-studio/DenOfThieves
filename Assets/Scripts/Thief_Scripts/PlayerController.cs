using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerController : MonoBehaviour, IPlayerController
{
    public bool IsMoving { get; set; }
    
    [SerializeField] private float lerpDuration = 0.5f;
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
        SpawnPlayer(startingTile);
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

    private void SpawnPlayer(Tile startingTile)
    {
        _player.SetCurrentTile(startingTile);
        transform.position = new Vector3(_player.CurrentTile.Coordinates.x, 0, _player.CurrentTile.Coordinates.z);
    }
    
    public void UpdatePlayerMovement()
    {
        var currentTileCoordinates = _player.CurrentTile.Coordinates;
        
        Coordinates upperCoordinates = new Coordinates(currentTileCoordinates.x - 1, currentTileCoordinates.z);
        Coordinates lowerCoordinates = new Coordinates(currentTileCoordinates.x + 1, currentTileCoordinates.z);
        Coordinates leftCoordinates = new Coordinates(currentTileCoordinates.x, currentTileCoordinates.z - 1);
        Coordinates rightCoordinates = new Coordinates(currentTileCoordinates.x, currentTileCoordinates.z + 1);

        UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType.Up, upperCoordinates);
        UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType.Down, lowerCoordinates);
        UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType.Left, leftCoordinates);
        UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType.Right, rightCoordinates);
    }
    
    private void UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType buttonType, Coordinates targetCoordinates)
    {
        Tile targetTile = _tileManager.GetTileFromCoordinates(targetCoordinates, _player.CurrentRoomId);
        
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
    
#if UNITY_EDITOR
    private void KeyMovement(Coordinates coordinates)
    {
        Tile targetTile = _tileManager.GetTileFromCoordinates(coordinates, _player.CurrentRoomId);

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

    public void MovePlayer(Tile targetTile)
    {
        if (targetTile is not null && !IsMoving)
        {
            StartCoroutine(LerpMove(targetTile));
        }
    }
    
    IEnumerator LerpMove(Tile targetTile)
    {
        IsMoving = true;
        Vector3 startLocation = _player.transform.position;
        Vector3 endLocation = new Vector3(targetTile.Coordinates.x, _player.transform.position.y, targetTile.Coordinates.z);
        float timeElapsed = 0;
        
        while (timeElapsed < lerpDuration)
        {
            transform.position = Vector3.Lerp(startLocation, endLocation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.position = endLocation;
        _player.SetCurrentTile(targetTile);
        UpdatePlayerMovement();
        IsMoving = false;
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
