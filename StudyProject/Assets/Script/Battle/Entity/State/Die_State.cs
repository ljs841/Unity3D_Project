using System;
using System.Collections.Generic;
using UnityEngine;

public class DIe_State : CharacterState
{
    public DIe_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _char.SetTargetVelocity_X(0);
        _char.SetDead();
        _char.AniControl.PlayAnimation(eAnimationStateName.Die);

        if(_char.AllyType != eUnitAllyType.Player)
        {
            FxManager.Instance.GetFx(eFxType.DIe, _char.Node.Middle.position);
        }
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

    }

    public override void OnAnimationPlayEnd(eAnimationStateName name)
    {
        if(name == _stateName)
        {
            if(_char.AllyType == eUnitAllyType.Player)
            {
                BattleManager._Instance.StageEnd();
            }
            if (_char.AllyType != eUnitAllyType.Player)
            {
                _char.DeActiveBehavior();
            }
        }
    }
}