using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValues.UIResPath;

public class UIManager 
{
    private static UIManager _instance;
    public static UIManager _Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }

    private BaseUILayer _mainUI;
    private BaseUILayer CreateMainUI()
    {
        if(_mainUI != null)
        {
            GameObject.Destroy(_mainUI);
        }

        var obj = (GameObject)ResourcesManager._Instance.CreateIntance<GameObject>(ConstValue._uiRes_BaseLayer);


        var objSc = obj.GetComponent<BaseUILayer>();
        if(objSc == null)
        {
            Util.DebugErrorLog("Script not attach is Null");
            return null;
        }
        return objSc;
    }

    public T CreateUIPrefab<T>(string path , eUILayer layer) where T : UIContentController
    {
        if(_mainUI == null)
        {
            _mainUI = CreateMainUI();
        }
        var obj = (GameObject)ResourcesManager._Instance.CreateIntance<GameObject>(path);

        if (obj == null)
            return null;
        _mainUI.AttachPrefab(obj, layer);
        return obj.AddComponent<T>();
    }





}
