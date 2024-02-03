using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDieState : GhostBaseState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Die State Enter");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fsm.ChangeState(GoToReturnStateName);
    }
}
