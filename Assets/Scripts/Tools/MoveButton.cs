using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MoveButton : MonoBehaviour
{
    public Tile affectedTile = null;
    private IPlayerController _playerController;

    [Inject]
    public void Init(IPlayerController playerController)
    {
        _playerController = playerController;
    }

    private void OnMouseDown()
    {
        if(affectedTile is not null)
        {
            _playerController.MovePlayer(affectedTile);
        }
    }

    public enum MoveButtonType
    {
        Up,
        Down,
        Left,
        Right
    }
}
