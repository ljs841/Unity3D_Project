using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUILayer : UIContentView
{
    public enum eUILayer
    {
        Layer1 = 0,
        Layer2,
        Layer3,
        Layer4
    }
    public List<Canvas> _canvasList;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _canvasList = new List<Canvas>();
    }

    public void AttachPrefab(GameObject prefabObj, eUILayer layer)
    {
        if((int)layer < 0 || _canvasList.Count >= (int)layer)
        {
            Debug.LogError("Layer value Wrong");
            return;
        }

        var obj = _canvasList[(int)layer];
        if(obj == null)
        {
            Debug.LogError(string.Format("{0} Canvas is Null", layer.ToString()));
            return;
        }
        Util.AttachGameObject(obj.gameObject, prefabObj);
    }




}
