using System;
using System.Collections.Generic;
using UnityEngine;

public class Crouch_State : CharacterState
{
    public Crouch_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _char.SetTargetVelocity(0,0);
        _char.AniControl.PlayAnimation(eAnimationStateName.Crouch);

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
               // ChangeState(eAnimationStateName.Run);
                break;
            case eInputType.Jump:
                ChangeState(eAnimationStateName.Jump);
                break;
            case eInputType.None:
                ChangeState(eAnimationStateName.Idle);
                break;
            case eInputType.Attak:
                ChangeState(eAnimationStateName.Crouch_Attack);
                break;
        }


    }
}