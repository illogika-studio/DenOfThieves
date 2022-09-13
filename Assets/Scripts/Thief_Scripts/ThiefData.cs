using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ThiefData
{
    [SerializeField] private int _maxActionPoints;
    public int MaxActionPoints => _maxActionPoints;
}
