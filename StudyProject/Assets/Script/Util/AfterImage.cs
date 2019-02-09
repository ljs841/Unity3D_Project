using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public SpriteAnimationController _dstRenderer;
    public List<AfterImageSub> _list = new List<AfterImageSub>();

    private void Start()
    {
        _dstRenderer.OnSpriteChange += OnChangeSprite;
        _dstRenderer.OnFlip += OnChangeFlip;
        foreach (var item in _list)
        {
            item.gameObject.transform.SetParent(null);
            item.gameObject.SetActive(true);
            item.PlayEffect();
        }

    }

    void OnChangeSprite(Sprite spr)
    {
        foreach (var item in _list)
        {
            item.SetSprite(spr);
        }
    }

    void OnChangeFlip(bool xFlip)
    {
        foreach (var item in _list)
        {
            item.SetFlip(xFlip);
        }
    }

    private void OnDisable()
    {
        _dstRenderer.OnSpriteChange -= OnChangeSprite;
        _dstRenderer.OnFlip -= OnChangeFlip;
        foreach (var item in _list)
        {
            item.gameObject.transform.SetParent(transform);
            item.gameObject.SetActive(false);
            item.StopEffect();
        }
    }

}
