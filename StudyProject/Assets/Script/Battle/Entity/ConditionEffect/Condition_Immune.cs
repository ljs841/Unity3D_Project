using System;
using System.Collections.Generic;
using UnityEngine;

public class Condition_Immune : ConditionEffect
{
    bool _flashOn;
    GameObject _obj;
    public Condition_Immune(eCondition effectNumber) :base(effectNumber)
    {
        _condition = effectNumber;
        SetData(TempData.GetConditionData(_condition));
        _flashOn = false;
    }

    public override void StartProcess()
    {
        base.StartProcess();
        _flashOn = false;
        _unit.SetImmune(true);
        _obj = FxManager.Instance.GetFx(eFxType.Immune , _unit.Node.Middle);
    }

    public override void EndProcess()
    {
        base.EndProcess();
        _unit.SetImmune(false);
        _obj.SetActive(false);
    }


}