using System;
using System.Collections.Generic;
using UnityEngine;

public class SingleToneMono<T> : MonoBehaviour where T : MonoBehaviour 
{
    protected static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                var ob = new GameObject();
                ob.transform.SetParent(SingleToneMonoParentObject.Instance.gameObject.transform);
                _instance = ob.AddComponent<T>();
            }
            return _instance;
        }
    }
}