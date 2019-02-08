using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    protected AnimationState_Machine _stateMachine;
    protected SimplePhysics _physics;
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

    protected Vector2 _velocity;
    public Vector2 Velocity
    {
        get
        {
            return _velocity;
        }
        set
        {
            _velocity = value;
        }
    }

    public Vector2 CurVelocity
    {
        get
        {
            return _physics.CurVelocity;
        }
    }
    public override void Init(eEntityType type, eEntityLookDir baseDir, int subType)
    {
        base.Init(type, baseDir, subType);
        var stat = TempData.GetCharacterData(type, subType);
        if (stat != null)
        {
            _stat = stat;
        }
        IsActive = true;
        _stateMachine = new AnimationState_Machine();
        _stateMachine.Init(this);
        _physics = new SimplePhysics(_rigidbody);
    }

    public override void StartEntity()
    {
        _stateMachine.ChangeState(eAnimationStateName.Idle);
        base.StartEntity();
    }

    public override void UpdateEntity()
    {
        base.UpdateEntity();
        _aniControl.SetSpriteFlip(_currentLookDir);
        _stateMachine.UpdateState();
        _physics.Velocity = _velocity;
    }

    public virtual void SetJumpForce(float value)
    {
        _physics.SetJumpForce(value);
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

    public void PublishInputEvent(InputManager input)
    {
        input.InputEvent += OnInputEvent;
    }


    public void UnPublishInputEvent(InputManager input)
    {
        input.InputEvent -= OnInputEvent;
    }

    void OnInputEvent(object sender, BattleInputEventArgs e)
    {
        switch (e.InputType)
        {
            case eInputType.LeftPress:

                _currentLookDir = eEntityLookDir.Left;
                break;
            case eInputType.RightPress:
                _currentLookDir = eEntityLookDir.Right;
                break;
            default:
                break;

        }
        _stateMachine.OnInputEvent(e.InputType);


    }
}
