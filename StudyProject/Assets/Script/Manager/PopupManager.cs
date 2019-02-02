using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager
{    
    private static PopupManager _instance;
    public static PopupManager _Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PopupManager();
            }
            return _instance;
        }
    }

    private Queue<UIController> _queue;

    private PopupManager()
    {
        _queue = new Queue<UIController>();
    }

    public T CreateUIPrefab<T>(string path) where T : UIController
    {
        var obj = (GameObject)ResourcesManager.Instance.CreateIntance<GameObject>(path);

        if (obj == null)
            return null;

        var sc = obj.GetComponent<T>();
        if (sc == null)
        {
            Util.DebugErrorLog("Script not attach is Null");
            return null;
        }

        _queue.Enqueue(sc);
        return sc;
    }

    public void ContentPopupAllDestroy()
    {
        while(_queue.Count >=1)
        {
            DestroyQueueItem();
        }
        _queue.Clear();
    }

    public void DestroyQueueItem()
    {
        var item = _queue.Dequeue();
        if (item != null && item.gameObject != null)
        {
            item.DestroyGameObj();
        }
    }

}