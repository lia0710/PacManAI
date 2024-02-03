using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpreadState : GhostBaseState
{
    protected float totalTime = 7;
    protected float currentTime = 0;
    protected List<Vector2> pointsList = new List<Vector2>(); 
    protected Vector2 loc1 = new Vector2(8, 9);
    protected Vector2 loc2 = new Vector2(8, 6);
    protected Vector2 loc3 = new Vector2(5, 6);
    protected Vector2 loc4 = new Vector2(5, 9);
    protected Vector2 currentTarget = new Vector2(5, 6);

/*    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Spread State Enter");
        switch (roundCounter)
        {
            case 1:
                totalTime = 7; break;
            case 2:
                totalTime = 7; break;
            case 3:
                totalTime = 5; break;
            case 4:
                totalTime = 5; break;
        }
        controller.SetMoveToLocation(loc1);
    }*/

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("A ghost is in Spread State");
        currentTime += Time.deltaTime;
        if (currentTime >= totalTime)
        {
            roundCounter += 1;
            currentTime = 0;
            fsm.ChangeState(GoToChaseStateName);
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
    override public void PathCompleted()
    {
        bool next = false;
        int i = 0;
        foreach (Vector2 vec in pointsList)
        { 
            if(vec == currentTarget) 
            {
                if (i+1 < pointsList.Count)
                {
                    currentTarget = pointsList[i + 1];
                }
                else
                { 
                    currentTarget = pointsList[0];
                }
                controller.SetMoveToLocation(currentTarget);
                break;
            }
            i++;
        }
    }
}
