
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CharacterState : State
{
    protected Character _char;
    protected eAnimationStateName _stateName;
    public Action<eAnimationStateName> OnChangeState;
    public eAnimationStateName StateName
    {
        get
        {
            return _stateName;
        }
    }

    public CharacterState(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }
   
    public void Init(Character character)
    {
        _char = character;
    }

    public virtual void OnInputEvent(eInputType inputType)
    {

    }

    protected void ChageState(eAnimationStateName nextState)
    {
        if(OnChangeState != null)
        {
            OnChangeState(nextState);
        }
    }

}
