using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIC_ScrollViewItem : UIContentController
{
    protected int _scrollIndex;
    protected ScrollViewDataModel _data;
    protected GridInItemManager _itemManager;
    public int ScrollIndex
    {
        get
        {
            return _scrollIndex;
        }

        set
        {
            _scrollIndex = value;
        }
    }

    public GridInItemManager ItemManager
    {
        get
        {
            return _itemManager;
        }

        set
        {
            _itemManager = value;
        }
    }

    public override void Hide()
    {
        base.Hide();
        gameObject.transform.localPosition = ConstValues.ConstValue._screenOutPos;
    }

    public void UpdateData(ScrollViewDataModel data)
    {
        _data = data;
        var sc = (UIV_ScrollViewItem)_component;
        sc.SetData(_data);
    }

    public void ViewUpdate()
    {
        _component.VIewUpdate();
    }

    public void DeleteData()
    {
        _itemManager.DeleteData(_scrollIndex);
    }

}