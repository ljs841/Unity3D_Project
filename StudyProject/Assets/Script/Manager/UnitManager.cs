using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager
{
    Dictionary<int, Character> _unitDic;

    List< Projectile> _projectileList;
    private static UnitManager _instance;
    public static UnitManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UnitManager();
                _instance.Init();
            }
            return _instance;
        }
    }

    public void Init()
    {
        _unitDic = new Dictionary<int, Character>();
        _projectileList = new List<Projectile>();
    }

    public void AddUnit(int instanceID, Character unit)
    {
        if(_unitDic.ContainsKey (instanceID) == false)
        {
            _unitDic.Add(instanceID, unit);
        }
    }


    public void AddProjectile(Projectile tile)
    {
        if (_projectileList.Contains(tile) == false)
        {
            _projectileList.Add(tile);
        }
    }

    public Character GetChar(int instanceID)
    {
        if (_unitDic.ContainsKey(instanceID))
        {
            return _unitDic[instanceID];
        }
        return null;
    }

    public void SendTrapDameage(int instanceId , float dameage)
    {
        var unit = GetChar(instanceId);
        if (unit != null)
        {
            unit.OnDameage(null, unit, -unit.Forward , dameage);
            unit.AddCondition(eCondition.Immune);
        }
    }
    public void SendProjectileDameage(int instanceId , float dameage)
    {
        var unit = GetChar(instanceId);
        if (unit != null)
        {
            unit.OnDameage(null, unit, -unit.Forward, dameage);
        }
    }

    public Projectile GetMiisile()
    {
        foreach(var obj in _projectileList)
        {
            if(obj.gameObject.activeInHierarchy == false)
            {
                return obj;
            }
        }
        return null;
    }

    public void Clear()
    {
        _unitDic.Clear();

        _projectileList.Clear();
    }

}