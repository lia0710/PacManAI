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
    protected GhostController controller;
    //protected GameDirector director;
    protected float roundCounter = 1;

    /*private void OnDestroy()
    {
        GameDirector.Instance.GameStateChanged.RemoveListener(StateChanged);
        controller.pathCompletedEvent.RemoveListener(PathCompleted);
        controller.killedEvent.RemoveListener(Killed);
    }*/

    public override void Init(GameObject _owner, FSM _fsm)
    {
        //director = GameDirector.Instance;
        base.Init(_owner, _fsm);

        controller = _owner.GetComponent<GhostController>();

        //GameDirector.Instance.GameStateChanged.AddListener(StateChanged);
        //controller.pathCompletedEvent.AddListener(PathCompleted);
        //controller.killedEvent.AddListener(Killed);
        Debug.Assert(controller != null, $"{_owner.name} must have a GhostController Component");
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
