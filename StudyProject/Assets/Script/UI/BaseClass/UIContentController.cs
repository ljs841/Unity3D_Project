using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    protected UIView _component;

    public virtual void Create()
    {
    }


    public virtual void Show()
    {
        _component.Show();
    }

    public virtual void Hide()
    {
        _component.Hide();
    }

    public virtual void AnimationCallBack(string clipName)
    {

    }

    public virtual void DestroyGameObj()
    {
        _component.DestroyObject();
    }


}
