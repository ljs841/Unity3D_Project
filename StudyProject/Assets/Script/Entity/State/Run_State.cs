using System;
using System.Collections.Generic;

public class Run_State : CharacterState
{
    public Run_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _char.Speed = _char.Stat.MoveSpeed;
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
            case eInputType.Jump:
                ChageState(eAnimationStateName.Jump);
                break;
        }
    }
}