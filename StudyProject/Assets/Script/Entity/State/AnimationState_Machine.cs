using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState_Machine 
{
    Dictionary<eAnimationStateName, CharacterState> _aniDic;
    CharacterState _prvState;
    CharacterState _curState;
    CharacterState _reverseState;
    Character _char;
    public virtual void Init(Character character)
    {
        _char = character;
        _aniDic = new Dictionary<eAnimationStateName, CharacterState>();

        Idle_State idle = new Idle_State((int)eAnimationStateName.Idle);
        idle.Init(_char);
        Run_State run = new Run_State((int)eAnimationStateName.Run);
        run.Init(_char);
        Attack_State attack = new Attack_State((int)eAnimationStateName.Attack);
        attack.Init(_char);
        Jump_State jump = new Jump_State((int)eAnimationStateName.Jump);
        jump.Init(_char);

        _aniDic.Add(eAnimationStateName.Idle, idle);
        _aniDic.Add(eAnimationStateName.Run, run);
        _aniDic.Add(eAnimationStateName.Attack, attack);
        _aniDic.Add(eAnimationStateName.Jump, jump);
        
    }

    bool HasState(eAnimationStateName name)
    {
        return _aniDic.ContainsKey(name);
    }

    public void ChangeState(eAnimationStateName name)
    {
        if(HasState(name))
        {
            _reverseState = _aniDic[name];
        }
    }

    public virtual void UpdateState()
    {
        if(_reverseState != null)
        {
            if (_curState != null)
            {
                _curState.OnExit();
                _curState.OnChangeState -= OnRequestChangeState;
            }

            _prvState = _curState;
            _curState = _reverseState;
            _reverseState = null;

            _curState.OnChangeState += OnRequestChangeState;
            _curState.OnEnter();
        }
        _curState.OnExcute();
    }

    public virtual void OnInputEvent(eInputType inputType)
    {
        _curState.OnInputEvent(inputType);
    }

    public void OnRequestChangeState(eAnimationStateName nextState)
    {
        if (HasState(nextState))
        {
            _reverseState = _aniDic[nextState];
        }
    }

}
