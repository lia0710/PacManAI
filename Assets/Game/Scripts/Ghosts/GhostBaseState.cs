using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBaseState : FSMBaseState
{
    public string GoToSpreadStateName = "Spread";
    public string GoToFleeStateName = "RunAway";
    public string GoToChaseStateName = "ChasePlayer";
    public string GoToDieStateName = "Die";
    public string GoToReturnStateName = "ReturnToBase";
    public string GoToRespawnStateName = "Respawn";
    public float boundXRight = 8.0f;
    public float boundXLeft = -8.0f;
    public float boundYUp = 9.0f;
    public float boundYDown = -9.0f;
    protected bool ClydeChase = false;
    protected GhostController controller;
    protected float roundCounter = 1;
    protected float totalTime = 0;

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);
        controller = _owner.GetComponent<GhostController>();
        Debug.Assert(controller != null, $"{_owner.name} must have a GhostController Component");
    }

    public bool distanceFromPacman()
    {
        //8 tiles, true if not close enough, false if far
        float difx = controller.PacMan.position.x - controller.position.x;
        float dify = controller.PacMan.position.y - controller.position.y;
        float hyp = (float)Math.Sqrt(dify * dify + difx * difx);
        if (hyp < 8) { return true; }
        return false;
    }

    public virtual void StateChanged(GameDirector.States _state)
    {
        switch (_state)
        {
            case GameDirector.States.enState_PacmanInvincible:
                break;
        }
    }

    public virtual void PathCompleted()
    {
    }

    public virtual void Killed()
    {
    }
}
