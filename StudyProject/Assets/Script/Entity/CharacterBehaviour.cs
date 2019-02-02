using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : EntityBehaviour
{

    bool _leftUp = true;
    bool _rightUp = true;

    private CharacterStat _stat;
    public override void SetEntityBehaviourData(EntityBehaviourData data)
    {
        base.SetEntityBehaviourData(data);
        CharacterBehaviourData characterData = (CharacterBehaviourData)data;
        _stat = characterData._characterStat;
    }

    public override void ActiveBehavior(eEntityLookDir dir)
    {
        base.ActiveBehavior(dir);
        _aniController = GetComponent<SpriteAnimationController>();
        _delaySec = new WaitForSeconds(_logicIntevalSec);
        _currentLookDir = dir;
        StartCoroutine(coUpdate());

    }

    public override void DeActiveBehavior()
    {
        base.DeActiveBehavior();
        StopCoroutine(coUpdate());
    }

    IEnumerator coUpdate()
    {
        while (true)
        {
            SetSpriteFlip();
            SetPosition();
            yield return _delaySec;
            
        }
    }

    void SetSpriteFlip()
    {
        _aniController.SpriteRenderer.flipX = _currentLookDir == eEntityLookDir.Left ? true : false;
    }

    void SetPosition()
    {
        var pos = transform.localPosition;
        pos.x += LootDirToValue() * GetSpeed();

        transform.localPosition = pos;
    }

    float GetSpeed()
    {
        if(_leftUp && _rightUp)
        {
            return 0.0f;
        }
        return _stat.MoveSpeed * _logicIntevalSec;
    }

    float LootDirToValue()
    {
        return _currentLookDir == eEntityLookDir.Right ? 1 : -1;
    }

    public override void SetLookDir(eEntityLookDir dir)
    {
        _currentLookDir = dir;
    }

    public override void GetButtonEvent(eInputType inputEvenet)
    {
        switch (inputEvenet)
        {
            case eInputType.LeftPress:
                _leftUp = _leftUp ? false : true;
                break;
            case eInputType.LeftUp:
                _leftUp = _leftUp == false ? true : false;
                break;
            case eInputType.RightPress:
                _rightUp = _rightUp ? false : true;
                break;
            case eInputType.RightUp:
                _rightUp = _rightUp == false ? true : false;
                break;
        }
        if(_leftUp && _rightUp)
        {
            if(_aniController.GetCurrentAnimation() != eAnimationStateName.Idle)
            {
                _aniController.PlayAnimation(eAnimationStateName.Idle);
            }
        }
        else
        {
            if (_aniController.GetCurrentAnimation() != eAnimationStateName.Run)
            {
                _aniController.PlayAnimation(eAnimationStateName.Run);
            }
         
        }

    }

}
