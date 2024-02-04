using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GhostFleeState : GhostBaseState
{

    private Vector2 currentPosition = new Vector2();
    private Vector2 lastPosition = new Vector2();
    private string direction = "Up";
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentPosition.x = controller.position.x;
        currentPosition.y = controller.position.y;
        lastPosition.x = controller.position.x;
        lastPosition.y = controller.position.y;
        Vector2 currentRoundedPosition = new Vector2(Mathf.Round(controller.position.x), Mathf.Round(controller.position.y));
        controller.SetMoveToLocation(currentRoundedPosition);
        GameDirector.Instance.GameStateChanged.AddListener(StateChanged);
        controller.pathCompletedEvent.AddListener(PathCompleted);
        controller.killedEvent.AddListener(Killed);
        Debug.Log("Flee State Enter");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentPosition.x = controller.position.x;
        currentPosition.y = controller.position.y;
        if (currentPosition.x < lastPosition.x) { direction = "Left"; }
        if (currentPosition.x > lastPosition.x) { direction = "Right"; }
        if (currentPosition.y < lastPosition.y) { direction = "Down"; }
        if (currentPosition.y > lastPosition.y) { direction = "Up"; }
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
        controller.pathCompletedEvent.RemoveListener(PathCompleted);
        controller.killedEvent.RemoveListener(Killed);
    }

    override public void PathCompleted()
    {
        Vector2 currentRoundedPosition = new Vector2(Mathf.Round(controller.position.x), Mathf.Round(controller.position.y));
        float ran = UnityEngine.Random.Range(0, 4);
        switch (ran)
        {
            case 0:
                currentRoundedPosition.y += 3;
                direction = "Up";
                break;
            case 1:
                currentRoundedPosition.y -= 3;
                direction = "Down";
                break;
            case 2:
                currentRoundedPosition.x -= 3;
                direction = "Left";
                break;
            case 3:
                currentRoundedPosition.x += 3;
                direction = "Right";
                break;
        }
        controller.SetMoveToLocation(currentRoundedPosition);
    }
}
