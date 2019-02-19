using System;
using System.Collections.Generic;
using UnityEngine;

public class Rise_State : CharacterState
{
    public Rise_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        _char.AniControl.PlayAnimation(eAnimationStateName.Rise);
    }

    public override void OnExcute()
    {
        base.OnExcute();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnAnimationPlayEnd(eAnimationStateName name)
    {
        if (name == _stateName)
        {
        }
    }
}