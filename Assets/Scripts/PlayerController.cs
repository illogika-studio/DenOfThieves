using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BaseThief _player;

    private void Start()
    {
        Debug.Log(_player.name);
    }

    public void SetPlayer(BaseThief player)
    {
        _player = player;
    }
}
