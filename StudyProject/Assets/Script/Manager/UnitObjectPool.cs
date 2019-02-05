using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitObjectPool : MonoBehaviour
{

    Queue<GameObject> _pool;
    
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
        _pool = new Queue<GameObject>();
        
    }

    public void AddResources(GameObject obj)
    {
        obj.transform.SetParent(transform, false);
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }

    public GameObject GetCharacterGameObject()
    {
        GameObject obj = _pool.Dequeue();
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.transform.SetParent(transform, false);
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }

}

