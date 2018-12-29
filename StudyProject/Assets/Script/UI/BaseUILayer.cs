﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUILayer : MonoBehaviour
{
    public Camera _UICamera;
    public List<GameObject> _CanvasList = new List<GameObject>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AttachPrefab(GameObject prefabObj, eUILayer layer)
    {
        if((int)layer < 0 || _CanvasList.Count <= (int)layer)
        {
            Util.DebugErrorLog("Layer value Wrong");
            return;
        }

        var obj = _CanvasList[(int)layer];
        if(obj == null)
        {
            Util.DebugErrorLog(string.Format("{0} Canvas is Null", layer.ToString()));
            return;
        }
        Util.AttachGameObject(obj, prefabObj , false , false);
    }

    public Camera GetUICamera()
    {
        return _UICamera;
    }




}
