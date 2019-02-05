using System;
using System.Collections.Generic;
using UnityEngine;

public class Jump_State : CharacterState
{
    float deltaTime = 0;
    Vector3 pos;
    public Jump_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _char.Speed = _char.Stat.MoveSpeed;
        _char.AniControl.PlayAnimation(eAnimationStateName.Idle);
        pos = _char.gameObject.transform.position;
    }

    public override void OnExcute()
    {
        base.OnExcute();

        var height = Mathf.Sin(Mathf.PI * deltaTime / 0.25f);
        deltaTime += Time.deltaTime ;        
        _char.gameObject.transform.position = pos + new Vector3( 0 , Mathf.Abs( height) , 0);
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
                ChageState(eAnimationStateName.Run);
                break;
        }


    }
}