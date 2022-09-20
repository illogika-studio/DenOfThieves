using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BasePlayer : MonoBehaviour
{
    [SerializeField] private MoveButton _moveButton;
    [SerializeField] private List<ActionSetup> _actionsSetupList = new List<ActionSetup>();
    
    [SerializeField] private GameObject _moveButtonHolder;
    //public GameObject MoveButtonHolder => _moveButtonHolder;

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
        foreach (var actionSetup in _actionsSetupList)
        {
            switch (actionSetup.ActionType)
            {
                case Action.ActionType.Move :
                {
                    _actionsList.Add(new MoveAction(_playerController ,actionSetup.Pattern, (int)transform.rotation.eulerAngles.y));
                    break;
                }
            }
        }
    }

    public void SetCurrentTile(Tile tile)
    {
        _currentTile = tile;
        SetCurrentRoom(tile);
        DestroyMoveButtons();
        UpdateActions();
    }
    
    private void SetCurrentRoom(Tile tile)
    {
        _currentRoomId = tile.RoomId;
    }

    public void UpdateActions()
    {
        foreach (Action action in _actionsList)
        {
            action.CreateActionButtons();
        }
    }

    public void CreateMoveButton(Tile targetTile, Coordinates offsetCoordinates)
    {
        if (targetTile is not null)
        {
            MoveButton moveButton = Instantiate(_moveButton, _moveButtonHolder.transform);
            moveButton.SetPlayerController(_playerController);
            moveButton.transform.position = 
                new Vector3((_playerController.GetTransform().position.x + 0.5f + offsetCoordinates.X), moveButton.transform.position.y, (_playerController.GetTransform().position.z + 0.5f + offsetCoordinates.Z));
            moveButton.AffectedTile = targetTile;
            
            if(offsetCoordinates.Z < 0)
            {
                if(offsetCoordinates.X > 0)
                {
                    moveButton.transform.eulerAngles = new Vector3(moveButton.transform.eulerAngles.x, -135, moveButton.transform.eulerAngles.z);
                }
                else if (offsetCoordinates.X < 0)
                {
                    moveButton.transform.eulerAngles = new Vector3(moveButton.transform.eulerAngles.x, -45, moveButton.transform.eulerAngles.z);
                }
                else
                {
                    moveButton.transform.eulerAngles = new Vector3(moveButton.transform.eulerAngles.x, -90, moveButton.transform.eulerAngles.z);
                }
            }else if(offsetCoordinates.Z > 0)
            {
                if (offsetCoordinates.X > 0)
                {
                    moveButton.transform.eulerAngles = new Vector3(moveButton.transform.eulerAngles.x, 135, moveButton.transform.eulerAngles.z);
                }
                else if (offsetCoordinates.X < 0)
                {
                    moveButton.transform.eulerAngles = new Vector3(moveButton.transform.eulerAngles.x, 45, moveButton.transform.eulerAngles.z);
                }
                else
                {
                    moveButton.transform.eulerAngles = new Vector3(moveButton.transform.eulerAngles.x, 90, moveButton.transform.eulerAngles.z);
                }
            }else if(offsetCoordinates.X > 0)
            {
                moveButton.transform.eulerAngles = new Vector3(moveButton.transform.eulerAngles.x, 180, moveButton.transform.eulerAngles.z);
            }
        }
    }

    public void HideMoveButtons(bool hide)
    {
        _moveButtonHolder.gameObject.SetActive(!hide);
    }

    private void DestroyMoveButtons()
    {
        foreach (Transform child in _moveButtonHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
