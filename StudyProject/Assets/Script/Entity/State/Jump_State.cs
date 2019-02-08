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
        _char.SetJumpForce(_char.Stat.JumpForce);

        _char.AniControl.PlayAnimation(eAnimationStateName.Jump);
    }

    public override void OnExcute()
    {
        base.OnExcute();
        if(_char.IsGround && _char.CurVelocity.y <= _char.Stat.JumpForce * 0.5f)
        {

            Debug.Log("jump end");
            ChageState(eAnimationStateName.Idle);
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
        }
    }

    float GetForward()
    {
        Vector3 vec = Util.Dir2DConvert3D(_char.CurrentLookDir);
        return vec.x;
    }

    void SetVelocity()
    {
        _char.Velocity = new Vector2(GetForward() * _char.Stat.MoveSpeed, _char.Velocity.y);
    }
}