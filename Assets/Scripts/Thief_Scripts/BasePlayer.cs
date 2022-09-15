using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePlayer : MonoBehaviour
{
    private Tile _currentTile;
    private Room _currentRoom;
    public Tile CurrentTile => _currentTile;
    public Room CurrentRoom => _currentRoom;

    [SerializeField] private MoveButton rightButton;
    [SerializeField] private MoveButton leftButton;
    [SerializeField] private MoveButton upButton;
    [SerializeField] private MoveButton downButton;

    public void SetCurrentTile(Tile tile)
    {
        _currentTile = tile;
        transform.position = new Vector3(_currentTile.Coordinates.x, 0, _currentTile.Coordinates.z);
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
