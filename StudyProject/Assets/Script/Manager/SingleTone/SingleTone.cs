using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleTone<T> where T : new()
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }


}