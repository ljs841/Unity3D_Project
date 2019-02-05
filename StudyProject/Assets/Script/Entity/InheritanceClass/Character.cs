using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    protected Move _move;
    protected CharacterStat _stat;
    AnimationState_Machine _stateMachine;
    public CharacterStat Stat
    {
        get
        {
            return _stat;
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
        MovePosition();
    }


    public override void MovePosition()
    {
        var pos = transform.position;
        pos += Util.CalResultPosition(Util.Dir2DConvert3D(_currentLookDir), Speed *_logicIntevalSec);
        transform.position = pos;
    }


    public void PublishInputEvent(InputManager input)
    {
        input.InputEvent += OnInputEvent;
    }


    public void UnPublishInputEvent(InputManager input)
    {

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
