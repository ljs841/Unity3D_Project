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
                ChageState(eAnimationStateName.Idle);
                break;
            case eInputType.LeftPress:                
            case eInputType.RightPress:
                SetVelocity();
                break;
            case eInputType.Jump:
                ChageState(eAnimationStateName.Jump);
                break;
        }
    }

    void SetVelocity()
    {
        _char.Velocity = new Vector2(GetForward() * _char.Stat.MoveSpeed, _char.Velocity.y);
    }


    float GetForward()
    {
        Vector3 vec = Util.Dir2DConvert3D(_char.CurrentLookDir);
        return vec.x;
    }
}