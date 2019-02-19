using System;
using System.Collections.Generic;
using UnityEngine;

public class Hide_State : CharacterState
{
    public Hide_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        base.OnEnter();

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
                ChangeState(eAnimationStateName.Run);
                break;
            case eInputType.Jump:
                ChangeState(eAnimationStateName.Jump);
                break;
            case eInputType.Crouch:
                ChangeState(eAnimationStateName.Crouch);
                break;
            case eInputType.Attak:
                ChangeState(eAnimationStateName.Attack);
                break;


        }


    }
}