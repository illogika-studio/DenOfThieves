using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerController
{
    public void SetPlayer(BasePlayer player);
    public void MovePlayer(Tile startingTile);
    public Transform GetTransform();
}
