using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitObjectPool : MonoBehaviour
{

    Queue<CharacterBehaviour> _pool;
    
    private static UnitObjectPool _instance;
    public static UnitObjectPool _Instance
    {
        get
        {
            if(_instance == null)
            {
                var obj = new GameObject("UnitObjectPool");
                _instance = obj.AddComponent<UnitObjectPool>();
                _instance.Init();

                
            }
            return _instance;
        }

    }

    private void Init()
    {
        _pool = new Queue<CharacterBehaviour>();
        
    }

    public void AddResources(CharacterBehaviour obj)
    {
        obj.transform.SetParent(transform, false);
        obj.DeActiveBehavior();
        _pool.Enqueue(obj);
    }

    public CharacterBehaviour GetCharacterGameObject()
    {
        CharacterBehaviour obj = _pool.Dequeue();
        return obj;
    }

    public void ReturnObject(CharacterBehaviour obj)
    {
        obj.transform.SetParent(transform, false);
        obj.DeActiveBehavior();
        _pool.Enqueue(obj);
    }

}

