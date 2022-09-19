using System;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public abstract class Action
{
    public enum ActionType
    {
        Move
    }

    public abstract ActionType ActionTypeEnum { get; set; }
    public abstract string DisplayName { get; set; }
    
    public abstract void DoAction();
}
