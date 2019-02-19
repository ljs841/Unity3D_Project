using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{

    protected Dictionary< eCondition , ConditionEffect> _conditionEffectDic;
    
    protected AnimationStateManager _stateManager;
    public AnimationStateManager StateManager
    {
        get
        {
            return _stateManager;
        }
    }
    protected AIStateManager _aiStateManager;
    public AIStateManager AIStateManager
    {
        get
        {
            return _aiStateManager;
        }
        set
        {
            _aiStateManager = value;
            
        }
    }

    protected CharacterStat _stat;
    public CharacterStat Stat
    {
        get
        {
            return _stat;
        }
    }
    public bool IsGround
    {
        get
        {
            return _physics.IsGround;
        }
    }
    public bool IsJumpState
    {
        get
        {
            return _physics.isJumpState;
        }
    } 
   

    protected Vector2 _targetVelocity;
    public Vector2 TargetVelocity
    {
        get
        {
            return _targetVelocity;
        }
    }

    protected Vector2 _reverseVelocity;
    public Vector2 ReverseVelocity
    {
        get
        {
            return _reverseVelocity;
        }
    }


    protected BoxCollider2D _collider;
    public BoxCollider2D Collider
    {
        get
        {
            return _collider;
        }

        set
        {
            _collider = value;
        }
    }

    protected float _curHP;
    public float HP
    {
        get
        {
            return _curHP;
        }
        set
        {
            _curHP = value;
            _curHP = _curHP <= 0 ? 0 : _curHP;
        }
    }

    protected bool _isImmune = false;
    public bool IsImmune
    {
        get
        {
            return _isImmune;
        }
    }

    protected bool _isDie = false;
    public bool IsDie
    {
        get
        {
            return _isDie;
        }

    }
    public Action<float> OnHpUpdate;

    List<ConditionEffect> _conditionList;
    public override void Init(eEntityType type, eEntityLookDir baseDir, int subType)
    {
        base.Init(type, baseDir, subType);

        var stat = TempData.GetCharacterData(type, subType);
        if (stat != null)
        {
            _stat = stat;
        }
        IsActive = true;
        _stateManager = new AnimationStateManager();
        _stateManager.Init(this);
        _physics = new SimplePhysics(_rigidbody , _node );
        _physics.AttackColliderList += OnAttackCollider;
        _aniControl.OnAnimationPlayEnd += OnAnimationPlayEnd;
        _aniControl.OnAnimationEvent += OnAnimationEvent;
        _conditionEffectDic = new Dictionary<eCondition, ConditionEffect>();
        _conditionList = new List<ConditionEffect>();
        _curHP = stat.MaxHp;

        SetForward(Util.Dir2DConvert3D(baseDir));
    }
   
    public override void StartEntity()
    {

        _stateManager.ChangeState(eAnimationStateName.Idle);
        if (_aiStateManager != null)
        {
            _aiStateManager.StartAi();
        }
        base.StartEntity();
    }

    public override void UpdateEntity()
    {
        base.UpdateEntity();
        _stateManager.UpdateState();
        UpdateCondition();
        _physics.TragetVelocity = _targetVelocity;
    }

    public void AddCondition(eCondition condition)
    {
        if(_conditionEffectDic.ContainsKey(condition))
        {
            _conditionEffectDic[condition].AddTime();
            _conditionEffectDic[condition].AddValue();
        }
        else
        {
            var conditionObj = ConditionFactory._Instance.CreateCondition(condition);
            conditionObj.AcceptCondition(this);
            _conditionEffectDic.Add(condition, conditionObj);
        }
       
    }
    public void UpdateCondition()
    {

        foreach (var ob in _conditionEffectDic.Values)
        {
            ob.UpdateCondition(Time.deltaTime);
            if (ob.IsEffectEnd)
            {
                _conditionList.Add(ob);
                continue;
            }
        }
        foreach (var ob in _conditionList)
        {
            _conditionEffectDic.Remove(ob.Condition);
        }
        _conditionList.Clear();
    }

    public virtual void SetJump(float value)
    {
        _physics.SetJump(value);
    }

    public override void FixedUpdateEntity()
    {
        base.FixedUpdateEntity();
        _physics.PhysicsUpdate();
    }

    private void FixedUpdate()
    {
        FixedUpdateEntity();
    }

    public void SetImmune(bool isImmune)
    {
        _isImmune = isImmune;
    }

    public void PublishInputEvent(InputManager input)
    {
        input.InputEvent += OnInputEvent;
    }


    public void SetTargetVelocity_X(float setSpeed)
    {
        _targetVelocity.x = setSpeed;
    }

    public void SetTargetVelocity_Y(float setSpeed)
    {
        _targetVelocity.y = setSpeed;
    }
    public void SetTargetVelocity(float xSpeed , float ySpeed)
    {
        SetTargetVelocity_X(xSpeed);
        SetTargetVelocity_Y(ySpeed);
    }


    public void SetReverseVelocity(Vector2 vec)
    {
        _reverseVelocity = vec.normalized; ;
    }

    public void ResetReverseVelocity()
    {
        _reverseVelocity = Vector2.zero;
    }

    public void UnPublishInputEvent(InputManager input)
    {
        input.InputEvent -= OnInputEvent;
    }

    public void SetAttack(bool isState)
    {
        _physics.SetAttack(isState);
    }

    protected void OnAnimationPlayEnd(eAnimationStateName name)
    {
        _stateManager.OnAnmationPlayEnd(name);
    }

    protected void OnAnimationEvent(eAnimationStateName name)
    {
        _stateManager.OnAnmationEvent(name);
    }

    public void OnDameage(Character sender , Character receiver , Vector2 dameageDir , float dameage)
    {
        if(receiver == this && IsImmune == false && IsDie == false)
        {
            SetReverseVelocity(dameageDir);
            _stateManager.ChangeState(eAnimationStateName.Hurt);
            var _obj = FxManager.Instance.GetFx(eFxType.Hit1, _node.Middle);
            _curHP -= dameage;
            if(_curHP <= 0)
            {
                if(_aiStateManager != null)
                {
                    _aiStateManager.StopAI();
                }
                _stateManager.ChangeState(eAnimationStateName.Die);
            }

            OnHpUpdate?.Invoke(_curHP / _stat.MaxHp);
        }
    }

    public void SetDead()
    {
        _isDie = true;
    }

    protected void OnAttackCollider(List<RaycastHit2D> hitList)
    {
        _stateManager.OnAttackCollider(hitList);
    }

    protected void OnInputEvent(object sender, BattleInputEventArgs e)
    {
        if(_stateManager.GetCurrentState() == eAnimationStateName.Hurt)
        {
            return;
        }

        SetForward(new Vector2(e.X_MoveDir, 0));
        _stateManager.OnInputEvent(e.InputType);
    }
}
