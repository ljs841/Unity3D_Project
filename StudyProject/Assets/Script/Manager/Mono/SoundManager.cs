using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingleToneMono<SoundManager>
{
    AudioSource _backgroundSource;
    AudioSource _sfxSource;
    AudioClip _bgmClip;
    Dictionary<eSoundType, AudioClip> _soundDic;
    public void Awake()
    {
        if (_backgroundSource == null)
        {
            _backgroundSource = gameObject.AddComponent<AudioSource>();
        }
        if (_sfxSource == null)
        {
            _sfxSource = gameObject.AddComponent<AudioSource>();
        }
        _soundDic = new Dictionary<eSoundType, AudioClip>();
    }

    public void AddClip(string assetName , AudioClip clip)
    {
        var data = TempData.GetSoundDate(assetName);
        if(_soundDic.ContainsKey(data._type) == false)
        {
            _soundDic.Add(data._type, clip);
 
        }
    }

    public void PlayBgm()
    {
        if(_soundDic.ContainsKey(eSoundType.Background))
        {
            AudioClip clip = _soundDic[eSoundType.Background];
            _backgroundSource.clip = clip;
            _backgroundSource.Play();
            _backgroundSource.volume = 0.3f;

        }
    }

    public void StopBGM()
    {
        _backgroundSource.Stop();
    }

    public void PlaySFX(eSoundType type)
    {
        if (_soundDic.ContainsKey(type))
        {
            AudioClip clip = _soundDic[type];
            //_sfxSource.clip = clip;
            _sfxSource.PlayOneShot(clip , 0.4f);

        }
    }
}