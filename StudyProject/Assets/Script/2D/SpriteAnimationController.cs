﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationController : MonoBehaviour
{
    public SpriteAnimationContainer _aniContainer;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return _spriteRenderer;
        }
    }

    Dictionary<eAnimationStateName, SpriteAnimationInfo> _sprAniInfo;
    public eAnimationStateName _currentAni;

    public Action<Sprite> OnSpriteChange;
    public Action<bool> OnFlip;

    SpriteAnimationInfo _playAniInfo = null;
    WaitForSeconds _delaySec;
    int _frameCount = 0;

    public void Init()
    {
        _sprAniInfo = new Dictionary<eAnimationStateName, SpriteAnimationInfo>();
        var list = _aniContainer.spriteAnimationInfoList;
        foreach(SpriteAnimationInfo value in list)
        {
            if(_sprAniInfo.ContainsKey(value.eAniState) == false)
            {
                _sprAniInfo.Add(value.eAniState, value);
            }
        }
 
    }

    public eAnimationStateName GetCurrentAnimation()
    {
        if(_playAniInfo != null)
        {
            return _playAniInfo.eAniState;
        }
        return eAnimationStateName.None;
    }

    public bool PlayAnimation(eAnimationStateName name)
    {
       
        if (_sprAniInfo.ContainsKey(name))
        {
            _playAniInfo = _sprAniInfo[name];
            _delaySec = new WaitForSeconds(_playAniInfo.NextSprChangePerSec);
            _frameCount = 0;
            StopAllCoroutines();
            StartCoroutine(AnimationPlay());
            return true;
        }
        return false;
    }

    public void StopAnimation()
    {
        _playAniInfo = null;
        _frameCount = 0;
    }

    public void SetSpriteFlip(eEntityLookDir currenrLookDir)
    {
        _spriteRenderer.flipX = currenrLookDir == eEntityLookDir.Left ? true : false;
        Flip(_spriteRenderer.flipX);
    }

    void SpriteChange(Sprite spr)
    {
        if(OnSpriteChange != null)
        {
            OnSpriteChange(spr);
        }
    }

    void Flip(bool xFlip)
    {
        if(OnFlip != null)
        {
            OnFlip(xFlip);
        }
    }

    IEnumerator AnimationPlay()
    {
        while (IsAnimationEnd() == false)
        {
            Sprite spr = _playAniInfo.GetSprite(_frameCount);
            _spriteRenderer.sprite = spr;
            SpriteChange(spr);
            yield return _delaySec;
            _frameCount++;

            if (_playAniInfo.isEndSprite(_frameCount))
            {
                _frameCount = _playAniInfo.IsLoopAnimation == false ? _frameCount-- : 0;
            }
        }
    }
    bool IsAnimationEnd()
    {
        if(_playAniInfo.IsLoopAnimation)
        {
            return false;
        }
        else
        {
            return _playAniInfo.isEndSprite(_frameCount);
        }
    }

}
