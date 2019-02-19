using System;
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

    public Action<eAnimationStateName> OnAnimationPlayEnd;
    public Action<eAnimationStateName> OnAnimationEvent;

    SpriteAnimationInfo _playAniInfo = null;
    WaitForSeconds _delaySec;
    int _frameCount = 0;

    public void Init()
    {
        _sprAniInfo = new Dictionary<eAnimationStateName, SpriteAnimationInfo>();
        var list = _aniContainer.SpriteAnimationInfoList;
        foreach(SpriteAnimationInfo value in list)
        {
            if(_sprAniInfo.ContainsKey(value.eAniState) == false)
            {
                _sprAniInfo.Add(value.eAniState, value);
            }
        }
 
    }


    public bool HasAnimationInfo(eAnimationStateName name)
    {
        foreach(var obj in _aniContainer.SpriteAnimationInfoList)
        {
            if (obj.eAniState == name)
            {
                return true;
            }
        }
        return false;
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

    public void SetSpriteFlip(eEntityLookDir baseLookDir, Vector2 vec)
    {
        int x = (int)vec.x;

        bool xflip = _spriteRenderer.flipX;
        Vector2 dd = Util.Dir2DConvert3D(baseLookDir);
        switch (x)
        {
            case 1:
                if(baseLookDir == eEntityLookDir.Left)
                {
                    xflip = true;
                }
                else if(baseLookDir == eEntityLookDir.Right)
                {
                    xflip = false;
                }
                break;
            case -1:
                if (baseLookDir == eEntityLookDir.Left)
                {
                    xflip = false;
                }
                else if (baseLookDir == eEntityLookDir.Right)
                {
                    xflip = true;
                }
                break;
            case 0:
                break;
        }
        _spriteRenderer.flipX = xflip;


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
        while (IsAnimationEndFame() == false)
        {
            Sprite spr = _playAniInfo.GetSprite(_frameCount);
            _spriteRenderer.sprite = spr;
            SpriteChange(spr);
            if(_playAniInfo.EventFrameCount == _frameCount)
            {
                AnimationEvent(_playAniInfo.eAniState);
            }
            if(_playAniInfo.SoundPlayList != null && _playAniInfo.SoundPlayList.Count > _frameCount)
            {
                SoundManager.Instance.PlaySFX((eSoundType)_playAniInfo.SoundPlayList[_frameCount]);
            }
            yield return _delaySec;
            _frameCount++;

            if (_playAniInfo.isEndSprite(_frameCount))
            {
                AnimationLastFrame(_playAniInfo.eAniState);
                _frameCount = _playAniInfo.IsLoopAnimation == false ? _frameCount-- : 0;
            }
        }
    }

    void AnimationLastFrame(eAnimationStateName name)
    {
        if(OnAnimationPlayEnd != null)
        {
            OnAnimationPlayEnd(name);
        }
    }

    void AnimationEvent(eAnimationStateName name)
    {
        if (OnAnimationEvent != null)
        {
            OnAnimationEvent(name);
        }
    }

    bool IsAnimationEndFame()
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
