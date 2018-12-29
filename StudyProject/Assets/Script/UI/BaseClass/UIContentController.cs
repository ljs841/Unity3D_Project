using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContentController : MonoBehaviour
{
    public UIComponent _component;

    private UIComponent GetComponentScript<T>(GameObject prefabObject) where T : UIComponent
    {
        var obj = prefabObject.GetComponent<T>();
        if (obj == null)
        {
            Util.DebugLog("Object is Null");
            return null;
        }
        return obj;
    }

    public virtual void Init<T>(GameObject prefabObject) where T : UIComponent
    {

        _component = GetComponentScript<T>(prefabObject);
        _component.SetController(this);
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

}
