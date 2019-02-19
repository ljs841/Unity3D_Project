using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateManager 
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
        foreach (eAnimationStateName key in Enum.GetValues(typeof(eAnimationStateName)))
        {
            if(_char.AniControl.HasAnimationInfo(key))
            {
                CreateState(key);
            }
        }
    }

    void CreateState(eAnimationStateName name )
    {
        CharacterState state = null;
        switch (name)
        {
            case eAnimationStateName.Idle:
                state = new Idle_State((int)name);
                break;
            case eAnimationStateName.Run:
                state = new Run_State((int)name);
                break;
            case eAnimationStateName.Attack:
                state = new Attack_State((int)name);
                break;
            case eAnimationStateName.Jump:
                state = new Jump_State((int)name);
                break;
            case eAnimationStateName.Crouch:
                state = new Crouch_State((int)name);
                break;
            case eAnimationStateName.Crouch_Attack:
                state = new CrouchAttack_State((int)name);
                break;
            case eAnimationStateName.Jump_Attack:
                state = new JumpAttack_State((int)name);
                break;
            case eAnimationStateName.Hurt:
                state = new Hurt_State((int)name);
                break;
            case eAnimationStateName.Hide:
                state = new Hide_State((int)name);
                break;
            case eAnimationStateName.Die:
                state = new DIe_State((int)name);
                break;
            case eAnimationStateName.Rise:
                state = new Rise_State((int)name);
                break;
            default:
                break;
        }
        if (state == null)
            return;
        state.Init(_char);
        _aniDic.Add(name, state);
    }

    public bool HasState(eAnimationStateName name)
    {
        return _aniDic.ContainsKey(name);
    }

    public void ChangeState(eAnimationStateName name)
    {
        if(HasState(name) && _char.IsDie == false )
        {
            _reverseState = _aniDic[name];
        }
    }

    public eAnimationStateName GetCurrentState()
    {
        if(_curState == null)
        {
            return eAnimationStateName.None;
        }
        return _curState.StateName;
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
        if(_curState!= null)
        {
            _curState.OnExcute();
        }
    }

    public virtual void OnInputEvent(eInputType inputType)
    {
        _curState.OnInputEvent(inputType);
    }

    public virtual void OnAnmationPlayEnd(eAnimationStateName name)
    {
        _curState.OnAnimationPlayEnd(name);
    }

    public virtual void OnAnmationEvent(eAnimationStateName name)
    {
        _curState.OnAnimationEvent(name);
    }

    public virtual void OnAttackCollider(List<RaycastHit2D> hitList)
    {
        _curState.OnAttackCollider(hitList);
    }


    public void OnRequestChangeState(eAnimationStateName nextState)
    {
        if (HasState(nextState))
        {
            _reverseState = _aniDic[nextState];
        }
    }

}
