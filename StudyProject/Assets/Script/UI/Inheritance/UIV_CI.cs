using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIV_CI : UIComponent
{
    public Animator _animator;

    public override void AnimationEnd(string clipName)
    {
        SceneLoadManager._Instance.SceneLoad(ConstValues.SceneName.Login.ToString());
    }

}
