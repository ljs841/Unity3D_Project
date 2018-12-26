using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private string _mainUIPath = "UI/Prefab/MainUI";
    public BaseUILayer CreateMainUI()
    {
        if(_mainUI != null)
        {
            GameObject.Destroy(_mainUI);
        }

        var obj = (GameObject)ResourcesManager._Instance.CreateIntance<GameObject>(_mainUIPath);


        var objSc = obj.AddComponent<BaseUILayer>();
        if(objSc == null)
        {
            Debug.LogError("Script not attach is Null");
            return null;
        }
        return objSc;
    }




}
