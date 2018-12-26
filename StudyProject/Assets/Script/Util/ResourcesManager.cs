using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private static ResourcesManager _instance;
    public static ResourcesManager _Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("ResourcesManager");
                _instance = obj.AddComponent<ResourcesManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private Object ResourcesLoad <T> (string path) where T : Object
    {
        var res = Resources.Load(path) as Object;
        if (res == null)
        {
            Debug.LogError("res is Null");
            return null;
        }
        return res;
    }

    public Object CreateIntance <T> (string path) where T : Object
    {
        var gameObj = GameObject.Instantiate(ResourcesLoad<Object>(path));
        if (gameObj == null)
        {
            Debug.LogError("Object is Null");
            return null;
        }
        return gameObj;
    }
}
