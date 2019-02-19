using System;
using System.Collections.Generic;
using UnityEngine;

public class Hurt_State : CharacterState
{
    public Hurt_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("dd");
        Vector2 vec = _char.ReverseVelocity;
        _char.ResetReverseVelocity();
        _char.SetTargetVelocity(vec.x * (_char.Stat.MoveSpeed *0.25f), 3 );
        _char.AniControl.PlayAnimation(eAnimationStateName.Hurt);

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
        if(name == _stateName)
        {
            ChangeState(eAnimationStateName.Idle);
        }
    }
}