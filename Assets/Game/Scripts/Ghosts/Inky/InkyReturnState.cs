using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyReturnState : GhostBaseState
{
    private Vector2 home = new Vector2(0, 0);
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Return State Enter");
        controller.SetMoveToLocation(home);
        controller.pathCompletedEvent.AddListener(PathCompleted);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.pathCompletedEvent.RemoveListener(PathCompleted);
    }

    override public void PathCompleted()
    {
        fsm.ChangeState(GoToRespawnStateName);
    }
}
