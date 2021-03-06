﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField]
    protected UIController _controller;
    
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void DestroyObject()
    {
        Destroy(gameObject);
    }

    public virtual void AnimationEnd(string clipName)
    {
        _controller.AnimationCallBack(clipName);
    }

    public virtual void VIewUpdate()
    {

    }
}
