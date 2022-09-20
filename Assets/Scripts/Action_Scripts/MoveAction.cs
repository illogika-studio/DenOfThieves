using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoveAction : Action
{
    private IPlayerController _playerController;
    public override ActionType ActionTypeEnum { get; } = ActionType.Move;
    public override string DisplayName { get; } = "Move Action";

    private Pattern _pattern;
    public Pattern Pattern => _pattern;

    public MoveAction(IPlayerController playerController, Pattern pattern, int rotation)
    {
        _playerController = playerController;
        _pattern = pattern;
        _pattern.SetRotation(rotation);
    }

    public override void CreateActionButtons()
    {
        foreach(Coordinates coordinates in Pattern.CoodinatesAffected)
        {
            _playerController.CreateMoveButton(coordinates);
        }
    }
}
