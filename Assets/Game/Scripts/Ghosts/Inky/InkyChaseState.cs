using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyChaseState : GhostBaseState
{
    float totalTime = 0;
    float currentTime = 0;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameDirector.Instance.GameStateChanged.AddListener(StateChanged);
        Debug.Log("Chase State Enter");
        switch (roundCounter)
        {
            case 1:
                totalTime = 20; break;
            case 2:
                totalTime = 20; break;
            case 3:
                totalTime = 20; break;
            case 4:
                totalTime = 999; break;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("In Chase State");

        currentTime += Time.deltaTime;
        if ((currentTime) >= totalTime && (roundCounter < 4))
        {
            roundCounter += 1;
            currentTime = 0;
            fsm.ChangeState(GoToSpreadStateName);
        }
    }

    override public void StateChanged(GameDirector.States _state)
    {
        //Debug.Log("state change");
        switch (_state)
        {
            case GameDirector.States.enState_PacmanInvincible:
                fsm.ChangeState(GoToFleeStateName);
                break;
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameDirector.Instance.GameStateChanged.RemoveListener(StateChanged);
    }
}
