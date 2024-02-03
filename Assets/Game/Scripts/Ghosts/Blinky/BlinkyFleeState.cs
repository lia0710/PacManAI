using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyFleeState : GhostBaseState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameDirector.Instance.GameStateChanged.AddListener(StateChanged);
        controller.killedEvent.AddListener(Killed);
        controller.pathCompletedEvent.AddListener(PathCompleted);
        Debug.Log("Flee State Enter");
    }

    override public void Killed()
    {
        fsm.ChangeState(GoToDieStateName);
    }

    override public void StateChanged(GameDirector.States _state)
    {
        //Debug.Log("state change");
        switch (_state)
        {
            case GameDirector.States.enState_PacmanInvincible:
                return;
                break;
        }
        if (controller.isDead)
        {
            return;
        }
        Debug.Log("Blinky moving from flee to spread");
        fsm.ChangeState(GoToSpreadStateName);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameDirector.Instance.GameStateChanged.RemoveListener(StateChanged);
        controller.killedEvent.RemoveListener(Killed);
        controller.pathCompletedEvent.RemoveListener(PathCompleted);
        Debug.Log("Flee State Enter");
    }

    override public void PathCompleted()
    {
        //get your quadrant
        //get pacman's quadrant
        //move to a safe quadrant
    }



    //add listener to path completed, add pathcompleted, remove listener
}