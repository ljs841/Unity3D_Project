using System;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : SingleToneMono<FxManager>
{
    Dictionary<eFxType, List<GameObject>> _fxDic;
    
    private void Awake()
    {
        _fxDic = new Dictionary<eFxType, List<GameObject>>();
    }

    public void AddResoruces(eFxType fxType ,GameObject obj)
    {
        obj.SetActive(false);
        if (_fxDic.ContainsKey(fxType))
        {
            _fxDic[fxType].Add(obj);
        }
        else
        {
            List<GameObject> list = new List<GameObject>();
            list.Add(obj);
            _fxDic.Add(fxType, list);
        }
        obj.transform.SetParent(gameObject.transform);
        
    }

    public GameObject GetFx(eFxType type)
    {
        if(_fxDic.ContainsKey(type))
        {
            foreach(var ob in _fxDic[type])
            {
                if(ob.activeInHierarchy == false)
                {
                    return ob;
                }
            }
        }
        return null;
    }

    public GameObject GetFx(eFxType type , Transform setParent , bool isFxActive = true)
    {
        var obj = GetFx(type);

        if (obj == null || setParent == null)
            return null;

        Util.AttachGameObject(setParent.gameObject, obj, false, true);
        obj.SetActive(isFxActive);

        return obj;

    }
    public GameObject GetFx(eFxType type, Vector3 pos, bool isFxActive = true)
    {
        var obj = GetFx(type);

        if (obj == null)
            return null;
        obj.transform.position = pos;
        obj.SetActive(isFxActive);

        return obj;

    }
    public void Clear()
    {
        _fxDic.Clear();
    }
}