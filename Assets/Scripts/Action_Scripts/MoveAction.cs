using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{
    public override ActionType ActionTypeEnum { get; set; } = ActionType.Move;
    public override string DisplayName { get; set; } = "MoveAction";
    
    public override void DoAction()
    {
        Debug.Log("Do");
    }
 
}
