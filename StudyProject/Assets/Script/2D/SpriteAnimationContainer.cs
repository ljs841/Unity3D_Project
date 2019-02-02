using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationContainer : MonoBehaviour
{
    [SerializeField]
    private List<SpriteAnimationInfo> _spriteAnimationInfoList;

    public List<SpriteAnimationInfo> spriteAnimationInfoList
    {
        get
        {
            return _spriteAnimationInfoList;
        }
    }

    
}

[System.Serializable]
public class SpriteAnimationInfo
{   
    [SerializeField]
    private eAnimationStateName _eState;   
    [SerializeField]
    private List<Sprite> _spriteList;
    [SerializeField]
    private float _nextSprChangePerSec;
    [SerializeField]
    private bool _isLoopAnimation;

    public eAnimationStateName eAniState
    {
        get
        {
            return _eState;
        }
    }

    public List<Sprite> SpriteList
    {
        get
        {
            return _spriteList;
        }
    }

    public float NextSprChangePerSec
    {
        get
        {
            return _nextSprChangePerSec;
        }
    }

    public bool IsLoopAnimation
    {
        get
        {
            return _isLoopAnimation;
        }
    }

    public bool isEndSprite(int curArrayIdx)
    {
        if (_spriteList.Count <= curArrayIdx)
        {
            return true;
        }

        return false;
    }

    public Sprite GetSprite(int idx)
    {
        if(isEndSprite(idx))
        {
            return _spriteList[0];
        }
        return _spriteList[idx];
    }


}

