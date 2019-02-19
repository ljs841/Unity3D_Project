using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitObjectPool : MonoBehaviour
{
    private static UnitObjectPool _instance;
    public static UnitObjectPool Instance
    {
        get
        {
            if(_instance == null)
            {
                var ob = new GameObject("UnitObjectPool");
                _instance = ob.AddComponent<UnitObjectPool>();
            }
            return _instance;
        }
    }

    Dictionary<string, Queue<GameObject>> _poolDic;
   

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _poolDic = new Dictionary<string, Queue<GameObject>>();
    }

    public void AddResources(string assetName ,GameObject obj)
    {
        if(_poolDic.ContainsKey(assetName))
        {
            _poolDic[assetName].Enqueue(obj);
        }
        else
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            queue.Enqueue(obj);
            _poolDic.Add(assetName, queue);
        }

        obj.transform.SetParent(transform, false);
        obj.SetActive(false);
    }

    public GameObject GetCharacterGameObject(string assetName)
    {
        if (_poolDic.ContainsKey(assetName))
        {
            var ob = _poolDic[assetName].Dequeue();
            if(ob != null)
            {
                return ob;
            }
        }
        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.transform.SetParent(transform, false);
        obj.SetActive(false);
    }

}

