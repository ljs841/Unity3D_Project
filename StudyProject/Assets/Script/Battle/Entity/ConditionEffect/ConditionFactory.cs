using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionFactory
{
    private static ConditionFactory _instance;
    public static ConditionFactory _Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ConditionFactory();
            }
            return _instance;
        }

    }

    public ConditionEffect CreateCondition(eCondition condition)
    {

        switch (condition)
        {

            case eCondition.Immune:
                return new Condition_Immune(condition);
            case eCondition.D_O_T:
            case eCondition.H_O_T:
                break;
        }


        return null;
    }
}