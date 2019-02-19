using System;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack_State : Attack_State
{
    
    public JumpAttack_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        base.OnEnter();

    }

    public override void OnExcute()
    {

        if (_char.IsGround && _char.IsJumpState == false)
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
    }

    public override void OnAnimationPlayEnd(eAnimationStateName name)
    {
        base.OnAnimationPlayEnd(name);
    }


}