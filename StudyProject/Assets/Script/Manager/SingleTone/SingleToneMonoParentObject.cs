using System;
using System.Collections.Generic;
using UnityEngine;

public class SingleToneMonoParentObject : MonoBehaviour
{
    private static SingleToneMonoParentObject _instance;
    public static SingleToneMonoParentObject Instance
    {
        get
        {
            if(_instance == null)
            {
                var ob = new GameObject("ManagerContinaer");
                _instance = ob.AddComponent<SingleToneMonoParentObject>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}