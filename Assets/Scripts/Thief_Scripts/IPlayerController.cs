using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerController
{
    public bool IsMoving { get; set; }
    public void SetPlayer(BasePlayer player);
    public void MovePlayer(Tile startingTile);
    public Transform GetTransform();
    public void UpdatePlayerMovement();
}
