using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : SingleToneMono<ResourcesManager>
{

    void Awake()
    {
        gameObject.name = "ResourcesManager";
    }

    private Object ResourcesLoad <T> (string path) where T : Object
    {
        var res = Resources.Load(path) as T;
        if (res == null)
        {
            Util.DebugLog("res is Null");
            return null;
        }
        return res;
    }

    public Object CreateIntance <T> (string path) where T : Object
    {
        var obj = Instantiate(ResourcesLoad<Object>(path));
        if (obj == null)
        {
            Util.DebugLog("Object is Null");
            return null;
        }
        return obj;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
