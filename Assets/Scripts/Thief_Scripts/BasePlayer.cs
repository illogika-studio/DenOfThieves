using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BasePlayer : MonoBehaviour
{
    [SerializeField] private MoveButton rightButton;
    [SerializeField] private MoveButton leftButton;
    [SerializeField] private MoveButton upButton;
    [SerializeField] private MoveButton downButton;
    [SerializeField] private List<ActionSetup> _actionsSetupList = new List<ActionSetup>();
    
    private Tile _currentTile;
    public Tile CurrentTile => _currentTile;
    
    private int _currentRoomId;
    public int CurrentRoomId => _currentRoomId;
    
    private List<Action> _actionsList = new List<Action>();
    public List<Action> ActionsList => _actionsList;
    
    private ITileManager _tileManager;
    private IPlayerController _playerController;

    [Inject]
    public void Init(ITileManager tileManager, IPlayerController playerController)
    {
        _tileManager = tileManager;
        _playerController = playerController;
    }

    private void Awake()
    {
        foreach (var actionForInspector in _actionsSetupList)
        {
            switch (actionForInspector.ActionType)
            {
                case Action.ActionType.Move :
                {
                    _actionsList.Add(new MoveAction());
                    break;
                }
            }
        }
    }

    public void SetCurrentTile(Tile tile)
    {
        _currentTile = tile;
        SetCurrentRoom(tile);
        _playerController.UpdatePlayerMovement();
    }
    
    private void SetCurrentRoom(Tile tile)
    {
        _currentRoomId = tile.RoomId;
    }
    
    public void UpdateMoveButtonByTile(MoveButton.MoveButtonType buttonType, Tile targetTile)
    {
        MoveButton button = GetButton(buttonType);

        if (targetTile is null)
        {
            button.gameObject.SetActive(false);
            button.affectedTile = null;
        }
        else
        {
            button.gameObject.SetActive(true);
            button.affectedTile = targetTile;
        }
    }

    public void HideMoveButtons()
    {
        rightButton.gameObject.SetActive(false);
        leftButton.gameObject.SetActive(false);
        upButton.gameObject.SetActive(false);
        downButton.gameObject.SetActive(false);
    }

    private MoveButton GetButton(MoveButton.MoveButtonType buttonType)
    {
        switch(buttonType)
        {
            case (MoveButton.MoveButtonType.Up):
                return upButton;
            case (MoveButton.MoveButtonType.Down):
                return downButton;
            case (MoveButton.MoveButtonType.Left):
                return leftButton;
            case (MoveButton.MoveButtonType.Right):
                return rightButton;
        }
        return null;
    }
}
