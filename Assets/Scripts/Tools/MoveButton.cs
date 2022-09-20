using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MoveButton : MonoBehaviour
{
    private IPlayerController _playerController;
    public Tile AffectedTile = null;

    [Inject]
    public void Init(IPlayerController playerController)
    {
        _playerController = playerController;
    }

    private void OnMouseDown()
    {
        if(AffectedTile is not null)
        {
            _playerController.MovePlayer(AffectedTile);
            Destroy(gameObject);
        }
    }

    public void SetPlayerController(IPlayerController playerController)
    {
        _playerController = playerController;
    }

    public enum MoveButtonType
    {
        Up,
        Down,
        Left,
        Right
    }
}
