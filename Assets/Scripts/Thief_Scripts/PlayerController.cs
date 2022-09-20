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
    //[SerializeField] private MoveButton buttonToSpawn;

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
                KeyMovement(new Coordinates(currentTileCoordinates.X, currentTileCoordinates.Z - 1));
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                KeyMovement(new Coordinates(currentTileCoordinates.X, currentTileCoordinates.Z + 1));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                KeyMovement(new Coordinates(currentTileCoordinates.X - 1, currentTileCoordinates.Z));
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                KeyMovement(new Coordinates(currentTileCoordinates.X + 1, currentTileCoordinates.Z));
            }
        }
#endif
    }

    private void SpawnPlayer(Tile startingTile)
    {
        _player.SetCurrentTile(startingTile);
        transform.position = new Vector3(_player.CurrentTile.Coordinates.X, 0, _player.CurrentTile.Coordinates.Z);
    }
    
    public void UpdatePlayerMovement()
    {
        var currentTileCoordinates = _player.CurrentTile.Coordinates;
        
        Coordinates upperCoordinates = new Coordinates(currentTileCoordinates.X - 1, currentTileCoordinates.Z);
        Coordinates lowerCoordinates = new Coordinates(currentTileCoordinates.X + 1, currentTileCoordinates.Z);
        Coordinates leftCoordinates = new Coordinates(currentTileCoordinates.X, currentTileCoordinates.Z - 1);
        Coordinates rightCoordinates = new Coordinates(currentTileCoordinates.X, currentTileCoordinates.Z + 1);
        /*
        UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType.Up, upperCoordinates);
        UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType.Down, lowerCoordinates);
        UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType.Left, leftCoordinates);
        UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType.Right, rightCoordinates);*/
    }

    /*
    private void UpdateMoveButtonByCoordinates(MoveButton.MoveButtonType buttonType, Coordinates targetCoordinates)
    {
        Tile targetTile = GetAccessibleTile(targetCoordinates);
        _player.UpdateMoveButtonByTile(buttonType, targetTile);
    }*/
    
#if UNITY_EDITOR
    private void KeyMovement(Coordinates coordinates)
    {
        Tile targetTile = GetAccessibleTile(coordinates);

        MovePlayer(targetTile);
    }
#endif

    private Tile GetAccessibleTile(Coordinates coordinates)
    {
        Tile targetTile = _tileManager.GetTileFromCoordinates(coordinates, _player.CurrentRoomId);

        if (targetTile is null)
        {
            foreach (Tile tile in _player.CurrentTile.GetTilesThroughDoors())
            {
                if (tile.Coordinates == coordinates)
                {
                    targetTile = tile;
                }
            }
        }

        return targetTile;
    }

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
        _player.HideMoveButtons(true);
        Vector3 startLocation = _player.transform.position;
        Vector3 endLocation = new Vector3(targetTile.Coordinates.X, _player.transform.position.y, targetTile.Coordinates.Z);
        float timeElapsed = 0;
        
        while (timeElapsed < lerpDuration)
        {
            transform.position = Vector3.Lerp(startLocation, endLocation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.position = endLocation;
        _player.SetCurrentTile(targetTile);
        _player.HideMoveButtons(false);
        IsMoving = false;
    }
    
    public void CreateMoveButton(Coordinates offsetCoordinates)
    {
        Coordinates patternAffectedCoordinates = GetPlayerCoordinates() + offsetCoordinates;
        Tile targetTile = GetAccessibleTile(patternAffectedCoordinates);
        _player.CreateMoveButton(targetTile, offsetCoordinates);
    }

    public Coordinates GetPlayerCoordinates()
    {
        return _player.CurrentTile.Coordinates;
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