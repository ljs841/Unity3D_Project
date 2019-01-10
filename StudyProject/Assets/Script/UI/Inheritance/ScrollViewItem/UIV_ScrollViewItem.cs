using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIV_ScrollViewItem : UIComponent , IScrollViewItemData
{
    public Image _img;
    public Text _text;

    ScrollViewDataModel _data;

    void ChangeColor(Color color)
    {
        _img.color = color;
    }

    void ChangeText(string str)
    {
        _text.text = str;
    }

    public override void VIewUpdate()
    {
        base.VIewUpdate();
        if (_data == null)
            return;
        ChangeColor(_data.color);
        ChangeText(_data.userName);
    }

    public void SetData(ScrollViewDataModel data)
    {
        _data = data;
    }
}