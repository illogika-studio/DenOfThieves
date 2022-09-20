using System;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public abstract class Action: MonoBehaviour
{
    public enum ActionType
    {
        Move,
        Door
    }

    public abstract ActionType ActionTypeEnum { get; }
    public abstract string DisplayName { get; }
    public abstract void CreateActionButtons();
}
