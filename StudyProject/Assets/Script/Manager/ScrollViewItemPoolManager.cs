using System;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewItemPoolManager : MonoBehaviour
{
    private Queue<UIC_ScrollViewItem> _noUseQueue;
    private Queue<UIC_ScrollViewItem> _useQueue;

    private static ScrollViewItemPoolManager _instance;
    public static ScrollViewItemPoolManager _Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("ScrollViewItemPoolManager");
                _instance = obj.AddComponent<ScrollViewItemPoolManager>();
                _instance.Init();
            }
            return _instance;
        }
    }

    const int _addItemCount = 10;
    void Init()
    {
        _noUseQueue = new Queue<UIC_ScrollViewItem>();
        _useQueue = new Queue<UIC_ScrollViewItem>();
        DontDestroyOnLoad(gameObject);
    }

    public void CreatePool (int count , GameObject resource)
    {
        for(int i = 0; i < count + _addItemCount; i++)
        {
            var obj = Instantiate(resource);
            var sc = obj.GetComponent<UIC_ScrollViewItem>();
            Util.AttachGameObject(gameObject, obj, false, false);
            sc.Hide();
            _noUseQueue.Enqueue(sc);
        }
    }

    public UIC_ScrollViewItem GetItem(int idx)
    {
        
        if (_noUseQueue.Count > 0)
        {
            var item = _noUseQueue.Dequeue();
            item.ScrollIndex = idx;
            _useQueue.Enqueue( item);
            return item;
        }
        Util.DebugLog(idx.ToString());
        return CreateItem();
    }

    public void CheckNoUseItem(int startIndex , int endindex)
    {
        int maxCount = _useQueue.Count;
        int count = 0;
        while (count < maxCount)
        {
            var item = _useQueue.Dequeue();
            if(item.ScrollIndex < startIndex || item.ScrollIndex > endindex)
            {
                item.ScrollIndex = -1;
                item.Hide();
                Util.AttachGameObject(gameObject, item.gameObject, false, false);
                _noUseQueue.Enqueue(item);
            }
            else
            {
                _useQueue.Enqueue(item);
            }
            count++;
        }

    }

    public void RetunAllItem()
    {
        int maxCount = _useQueue.Count;
        int count = 0;
        while (count < maxCount)
        {
            var item = _useQueue.Dequeue();
            item.ScrollIndex = -1;
            item.Hide();
            Util.AttachGameObject(gameObject, item.gameObject, false, false);
            _noUseQueue.Enqueue(item);
            count++;
        }
    }
    
    public UIC_ScrollViewItem CreateItem()
    {
        Util.DebugErrorLog("over fllow");
        return null;
    }




}