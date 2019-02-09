using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageSub : MonoBehaviour
{
    public Transform _targetTrans;
    public SpriteRenderer _renderer;
    public Vector3 _offset;
    public float _dealySec;
    WaitForSeconds _waitSec;
    float xOffset = 0;
    public void PlayEffect()
    {
        _waitSec = new WaitForSeconds(_dealySec);
        StartCoroutine(coDealyUpdate());
    }

    public void SetSprite(Sprite spr)
    {
        _renderer.sprite = spr;
    }

    public void SetFlip(bool xFlip)
    {
        _renderer.flipX = xFlip;
        xOffset = xFlip ? _offset.x : -_offset.x;
    }

    IEnumerator coDealyUpdate()
    {
        while (true)
        {
            yield return _waitSec;
            var targetPos = _targetTrans.position ;
            targetPos.x += xOffset;
            transform.position = targetPos;

        }
    }

    public void StopEffect()
    {
        StopAllCoroutines();
    }
}
