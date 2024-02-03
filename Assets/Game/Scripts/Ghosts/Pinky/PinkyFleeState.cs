using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyFleeState : GhostBaseState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameDirector.Instance.GameStateChanged.AddListener(StateChanged);
        controller.killedEvent.AddListener(Killed);
        Debug.Log("Flee State Enter");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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

        fsm.ChangeState(GoToSpreadStateName);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameDirector.Instance.GameStateChanged.RemoveListener(StateChanged);
        controller.killedEvent.RemoveListener(Killed);
        Debug.Log("Flee State Enter");
    }
}
