using System;
using System.Collections.Generic;
using UnityEngine;

public class Jump_State : CharacterState
{
    public Jump_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _char.SetTargetVelocity_Y(_char.Stat.JumpForce);
        _char.SetJump(0);
        _char.AniControl.PlayAnimation(eAnimationStateName.Jump);
    }

    public override void OnExcute()
    {
        base.OnExcute();
        if(_char.IsGround &&_char.IsJumpState == false)
        {
            SoundManager.Instance.PlaySFX(eSoundType.Walk);
            ChangeState(eAnimationStateName.Idle);
        }
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
                SetVelocity();
                break;
            case eInputType.Attak:
                ChangeState(eAnimationStateName.Jump_Attack);
                break;
        }
    }

    void SetVelocity()
    {
        _char.SetTargetVelocity_X(_char.Forward.x * _char.Stat.MoveSpeed);
    }
}