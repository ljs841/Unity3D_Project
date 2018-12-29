using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    private static bool _isShowConsoleLog = true;

    public static void AttachGameObject(GameObject parent , GameObject prefabObj , bool worldPosStays , bool positionReset)
    {
        prefabObj.transform.SetParent(parent.transform, worldPosStays);
        if(positionReset)
        {
            prefabObj.transform.position = positionReset ? Vector3.zero : prefabObj.transform.position;
        }

    }

    public static void DebugLog(string msg)
    {
        if (_isShowConsoleLog == false)
            return;
        Debug.Log(msg);
    }

    public static void DebugErrorLog(string msg)
    {
        if (_isShowConsoleLog == false)
            return;
        Debug.LogError(msg);
    }

}

