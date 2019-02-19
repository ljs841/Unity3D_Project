using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIV_ScrollViewItem : UIView, IScrollViewItemData
{
    public Animator _aniController;
    public Image _img;
    public Text _text;

    ScrollViewDataModel _data;

    void ChangeColor(Color color)
    {
        if (_img == null)
            return;
        _img.color = color;
    }

    void ChangeText(string str)
    {
        if (_text == null)
            return;
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

    public void OnClickDelete()
    {

        _aniController.enabled = true;
        _aniController.Play("ScrollViewItemHide" , -1 , 0); ;
    }

    public void OnClickSelect()
    {
        
    }

    public override void AnimationEnd(string clipName)
    {
        if(clipName.Equals("ScrollViewItemHide"))
        {
            _aniController.Rebind();
           // var controller = (UIC_ScrollViewItem)_controller;
          //  controller.DeleteData();
        }

    }
}
