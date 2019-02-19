using System;
using System.Collections.Generic;
using UnityEngine;

public class Attack_State : CharacterState
{
    protected bool _isAnimationEnd = false;
    protected AnimationStateManager _stateManager;
    public AnimationStateManager StateManager
    {
        get
        {
            return _stateManager;
        }
    }
    public Attack_State(int stateIdx) : base(stateIdx)
    {
        _stateName = (eAnimationStateName)stateIdx;
    }

    public override void OnEnter()
    {
        _isAnimationEnd = false;
        if(_char.IsJumpState == false)
        {
            _char.SetTargetVelocity(0, 0);
        }
        _char.AniControl.PlayAnimation(_stateName);
    }

    public override void OnExcute()
    {
        base.OnExcute();
        if(_isAnimationEnd)
        { 
            _char.SetAttack(false);
            ChangeState(eAnimationStateName.Idle);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnAttackCollider(List<RaycastHit2D> hitList)
    {
        foreach(var hitObj  in hitList)
        {
            Character obj = UnitManager.Instance.GetChar(hitObj.collider.gameObject.GetInstanceID());
            if (obj != null && Util.CheckAlly(_char.AllyType, obj.AllyType) == false)
            {
                obj.OnDameage(_char, obj, _char.Forward, _char.Stat.BaseAtK);
            }
        }
        _char.SetAttack(false);
    }
    public override void OnAnimationEvent(eAnimationStateName name)
    {
        _char.SetAttack(true);
    }

    public override void OnAnimationPlayEnd(eAnimationStateName name)
    {
        if(name == _stateName)
        {
            if(_char.Stat.AttackType == eAttackType.Range)
            {
                Fire();
            }
            _isAnimationEnd = true;
            
        }
    }

    void Fire()
    {
        var m = UnitManager.Instance.GetMiisile();
        if (m != null)
        {
            var pos = _char.Node.RangeAttackNode.localPosition;
            pos.x *= -1;
            float distance = Vector3.Distance(_char.Node.RangeAttackNode.transform.localPosition, pos); ;
            Vector3 mPos = _char.Node.RangeAttackNode.transform.position;
            mPos.x = _char.Forward.x < 0 ? mPos.x - distance : mPos.x;


            m.transform.position = mPos;
            m.ActiveBehavior();
            m.FireProjectile(_char.Forward);
        }
    }

}