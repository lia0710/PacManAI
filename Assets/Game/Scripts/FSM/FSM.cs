using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM : MonoBehaviour
{
    public RuntimeAnimatorController FSMController;

    public Animator fsmAnimator { get; private set; }

    private void Awake()
    {
        GameObject FSMGo = new GameObject("FSM", typeof(Animator));
        FSMGo.transform.parent = transform;

        fsmAnimator = FSMGo.GetComponent<Animator>();
        fsmAnimator.runtimeAnimatorController = FSMController;

        FSMGo.hideFlags = HideFlags.HideInInspector;

        FSMBaseState[] behaviours = fsmAnimator.GetBehaviours<FSMBaseState>();
        foreach(FSMBaseState state in behaviours)
        {
            state.Init(gameObject, this);
        }
    }

    public bool ChangeState(string stateName)
    {
        return ChangeState(Animator.StringToHash(stateName));
    }

    public bool ChangeState(int hashStateName)
    {
        bool hasState = true;
#if UNITY_EDITOR
        hasState = fsmAnimator.HasState(0, hashStateName);
        Debug.Assert(hasState);
#endif
        fsmAnimator.CrossFade(hashStateName, 0.0f, 0);
        return hasState;
    }
    
}
