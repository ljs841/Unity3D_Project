using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionEffect
{
    protected int _conditionIdx;
    protected eCondition _condition;
    protected float _startTime;
    protected float _currentTime;
    protected float _addValue;
    protected ConditionData _data;
    protected Character _unit;
    protected bool _isEffectEnd = false;
    protected float _tickCount = 1;
    public bool IsEffectEnd
    {
        get
        {
            return _isEffectEnd;
        }
    }
    public eCondition Condition
    {
        get
        {
            return _condition;
        }
    }

    public ConditionEffect(eCondition effectNumber)
    {
        _condition = effectNumber;
    }

    protected void SetData(ConditionData data)
    {
        _data = data;
    }

    public void AcceptCondition(Character unit)
    {
        _startTime = Time.time;
        _unit = unit;
        _currentTime = 0;
        StartProcess();
        _isEffectEnd = false;
    }

    public virtual void StartProcess()
    {

    }

    public virtual void EndProcess()
    {

    }

    public void UpdateCondition(float deltaTime)
    {
        if(_isEffectEnd == false)
        {
            if(TickOn())
            {
                ConditionProcess();
            }
            _currentTime += deltaTime;  
            IsConditionEnd();
        }
    }

    protected bool TickOn()
    {
        if(_currentTime >= _data._timeTick * _tickCount)
        {
            return true;
        }
        return false;
    }

    public void AddTime()
    {
        if (_data._isAddtive == false)
            return;
        _startTime += _data._duration;
    }

    public void AddValue()
    {
        if (_data._isAddtive == false)
            return;
        _addValue += _data._conditionValue;
    }

    protected bool IsConditionEnd()
    {
        if(_startTime + _currentTime >= _startTime + _data._duration)
        {
            _isEffectEnd = true;
            EndProcess();
        }
        return true;
    }

    protected virtual void ConditionProcess()
    {

    }



}
