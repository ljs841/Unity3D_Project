using System;
using System.Collections.Generic;
using UnityEngine;
public class Run_State : CharacterState
{
    public Run_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        SetVelocity();
        _char.AniControl.PlayAnimation(eAnimationStateName.Run);
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
            case eInputType.NonMove:
                ChangeState(eAnimationStateName.Idle);
                break;
            case eInputType.LeftPress:
            case eInputType.RightPress:
                SetVelocity();
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

    void SetVelocity()
    {
        if(_char.ReverseVelocity != Vector2.zero)
        {
            Vector2 vec = _char.ReverseVelocity;
            _char.ResetReverseVelocity();
            _char.SetTargetVelocity_X(vec.x * _char.Stat.MoveSpeed );
        }
        else
        {
            _char.SetTargetVelocity_X(_char.Forward.x * _char.Stat.MoveSpeed);
        }
    }

}