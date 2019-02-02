using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValues;

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

        var obj = (GameObject)ResourcesManager.Instance.CreateIntance<GameObject>(ConstValue._baseLayer);


        var objSc = obj.GetComponent<BaseUILayer>();
        if(objSc == null)
        {
            Util.DebugErrorLog("Script not attach is Null");
            return null;
        }
        return objSc;
    }

    public T CreateUIPrefab<T>(string path , eUILayer layer) where T : UIController
    {
        if(_mainUI == null)
        {
            _mainUI = CreateMainUI();
        }

        var sc = PopupManager._Instance.CreateUIPrefab<T>(path);
        
        if(sc == null)
        {
            return null;
        }
        _mainUI.AttachPrefab(sc.gameObject, layer , false , false);
        sc.Create();
        return sc;
    }

    public void ContentPopupAllDestroy()
    {
        PopupManager._Instance.ContentPopupAllDestroy();
    }

    public void SceneForCameraSetting()
    {
        _mainUI.SetUICamera();
    }




}
