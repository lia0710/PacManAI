using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyBaseState : FSMBaseState
{
    public string GoToSpreadStateName = "Spread";
    public string GoToFleeStateName = "RunAway";
    public string GoToChaseStateName = "ChasePlayer";
    protected GhostController controller;
    //protected GameDirector director;
    protected float roundCounter = 1;
    private bool reachedTarget = false;

    void Start()
    {
        //public GameStateChangedEvent GameStateChanged; a variable storing the event. "can notify you when a game state changes"
}

    private void OnDestroy()
    {
        GameDirector.Instance.GameStateChanged.RemoveListener(StateChanged);
        controller.pathCompletedEvent.RemoveListener(PathCompleted);
    }

    public override void Init(GameObject _owner, FSM _fsm)
    {
        //director = GameDirector.Instance;
        base.Init(_owner, _fsm);

        GameDirector.Instance.GameStateChanged.AddListener(StateChanged);
        controller = _owner.GetComponent<GhostController>();
        controller.pathCompletedEvent.AddListener(PathCompleted);
        Debug.Assert(controller != null, $"{_owner.name} must have a GhostController Component");
    }

    public virtual void StateChanged(GameDirector.States _state)
    {
        Debug.Log("state change");
        switch (_state) 
        { 
            case GameDirector.States.enState_PacmanInvincible:
                break;
        }
    }

    public virtual void PathCompleted()
    {
        //Debug.Log("Path Complete");
    }
}
