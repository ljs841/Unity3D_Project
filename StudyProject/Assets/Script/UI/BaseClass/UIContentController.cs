using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContentController : MonoBehaviour
{
    public UIComponent _component;

    protected T GetComponentScript<T>() where T : UIComponent
    {
        var obj = gameObject.GetComponent<T>();
        if (obj == null)
        {
            Util.DebugLog("Object is Null");
            return null;
        }
        return obj;
    }

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

    public virtual void DestroyObject()
    {
        _component.DestroyObject();
    }

    public virtual void AnimationCallBack(string clipName)
    {

    }

    public virtual void DestroyGameObj()
    {
        Destroy(gameObject);
    }


}
