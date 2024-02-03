using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRespawnState : GhostBaseState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Respawn State Enter");
        controller.isDead = false;
        fsm.ChangeState(GoToSpreadStateName);
    }
}
