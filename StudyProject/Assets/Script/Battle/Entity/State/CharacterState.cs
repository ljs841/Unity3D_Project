
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CharacterState : State
{
    protected Character _char;
    protected eAnimationStateName _stateName;
    public eAnimationStateName StateName
    {
        get
        {
            return _stateName;
        }
    }

    public Action<eAnimationStateName> OnChangeState;

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

    public virtual void OnAnimationPlayEnd(eAnimationStateName name)
    {

    }
    
    public virtual void OnAnimationEvent(eAnimationStateName name)
    {

    }

    public virtual void OnAttackCollider(List<RaycastHit2D> hitList)
    {
    }

    protected void ChangeState(eAnimationStateName nextState)
    {
        if(OnChangeState != null)
        {
            OnChangeState(nextState);
        }
    }

}
