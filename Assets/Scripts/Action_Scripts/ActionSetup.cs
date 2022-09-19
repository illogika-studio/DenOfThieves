using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionSetup
{
    [SerializeField] 
    private Action.ActionType _actionsType;
    public Action.ActionType ActionType => _actionsType;
    
}
