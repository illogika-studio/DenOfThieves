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
    
    private bool _isMoving = false;

    private Tile _currentTile;
    public Tile CurrentTile => _currentTile;
    private Room _currentRoom;
    public Room CurrentRoom => _currentRoom;

    private ITileManager _tileManager;

    [Inject]
    public void Init(ITileManager tileManager)
    {
        _tileManager = tileManager;
    }

    public void SetCurrentTile(Tile tile, bool instant = false)
    {
        if (!_isMoving)
        {
            _currentTile = tile;

            Room tileRoom = _tileManager.GetRoomFromTile(_currentTile);

            if (tileRoom.Id != _currentRoom.Id)
            {
                _currentRoom = tileRoom;
            }

            if (instant)
            {
                transform.position = new Vector3(_currentTile.Coordinates.x, 0, _currentTile.Coordinates.z);
            }
            else
            {
                StartCoroutine(LerpMove(new Vector3(_currentTile.Coordinates.x, 0, _currentTile.Coordinates.z)));
            }
        }
    }

    IEnumerator LerpMove(Vector3 endLocation)
    {
        _isMoving = true;
        Vector3 startLocation = transform.position;

        float lerpDuration = 0.5f;
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            transform.position = Vector3.Lerp(startLocation, endLocation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = endLocation;
        _isMoving = false;
    }

    public void SetCurrentRoom(Room room)
    {
        _currentRoom = room;
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
