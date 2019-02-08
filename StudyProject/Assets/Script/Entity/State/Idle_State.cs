using System;
using System.Collections.Generic;
using UnityEngine;

public class Idle_State : CharacterState
{
    public Idle_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _char.Velocity = Vector2.zero;
        _char.AniControl.PlayAnimation(eAnimationStateName.Idle);

    }

    public override void OnExcute()
    {
        base.OnExcute();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnInputEvent(eInputType inputType)
    {
        switch (inputType)
        {
            case eInputType.LeftPress:
            case eInputType.RightPress:
                ChageState(eAnimationStateName.Run);
                break;
            case eInputType.Jump:
                ChageState(eAnimationStateName.Jump);
                break;

        }


    }
}