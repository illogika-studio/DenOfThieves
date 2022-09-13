using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Thief : MonoBehaviour
{
    [Inject] private ThiefData _thiefData;

    private void Awake()
    {
        Debug.Log(_thiefData.MaxActionPoints);
    }
}
