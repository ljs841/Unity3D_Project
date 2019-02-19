using System;
using System.Collections.Generic;
using UnityEngine;

public class CrouchAttack_State : Attack_State
{
    
    public CrouchAttack_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExcute()
    {
        if (_isAnimationEnd)
        {
            ChangeState(eAnimationStateName.Crouch);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnInputEvent(eInputType inputType)
    {
    }

    public override void OnAnimationPlayEnd(eAnimationStateName name)
    {
        base.OnAnimationPlayEnd(name);
    }


}