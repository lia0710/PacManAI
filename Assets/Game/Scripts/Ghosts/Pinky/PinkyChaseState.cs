using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyChaseState : GhostBaseState
{
    float totalTime = 0;
    float currentTime = 0;
    public float changeTimer = 1;
    float currentChangeTime = 0;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameDirector.Instance.GameStateChanged.AddListener(StateChanged);
        controller.pathCompletedEvent.AddListener(PathCompleted);
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
        controller.SetMoveToLocation(CalculateTarget());
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        currentTime += Time.deltaTime;
        if ((currentTime) >= totalTime && (roundCounter < 4))
        {
            roundCounter += 1;
            currentTime = 0;
            fsm.ChangeState(GoToSpreadStateName);
        }
        if (currentChangeTime > changeTimer)
        {
            controller.SetMoveToLocation(CalculateTarget());
            currentChangeTime = 0;
        }
    }

    override public void StateChanged(GameDirector.States _state)
    {
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
        controller.pathCompletedEvent.RemoveListener(PathCompleted);
    }

    override public void PathCompleted()
    {
        controller.SetMoveToLocation(CalculateTarget());
    }

    private Vector2 CalculateTarget()
    {
        Vector2 target = new Vector2(controller.PacMan.position.x, controller.PacMan.position.y);
        switch (GameDirector.Instance.pacmanController.moveDirection)
        {
            case PacmanController.MoveDirection.Left:
                target.x -= 4; break;
            case PacmanController.MoveDirection.Right:
                target.x += 4; break;
            case PacmanController.MoveDirection.Up:
                target.y += 4; break;
            case PacmanController.MoveDirection.Down:
                target.y -= 4; break;
        }
        return target;
    }
}
