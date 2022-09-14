using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    private Tile _currentTile;
    public Tile CurrentTile => _currentTile;

    public void SetCurrentTile(Tile tile)
    {
        _currentTile = tile;
        transform.position = _currentTile.transform.position;
    }
}
