using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationContainer : MonoBehaviour
{
    [SerializeField]
    private List<SpriteAnimationInfo> _spriteAnimationInfoList;

    public List<SpriteAnimationInfo> SpriteAnimationInfoList
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
    private List<int> _soundPlayList;
    [SerializeField]
    private float _nextSprChangePerSec;
    [SerializeField]
    private bool _isLoopAnimation;
    [SerializeField]
    private int _eventFrameCount;


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

    public int EventFrameCount
    {
        get
        {
            return _eventFrameCount;
        }
    }

    public List<int> SoundPlayList
    {
        get
        {
            return _soundPlayList;
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

