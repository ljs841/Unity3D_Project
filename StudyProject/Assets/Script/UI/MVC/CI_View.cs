using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CI_View : UIView
{
    public Animator _animator;

    public override void AnimationEnd(string clipName)
    {
        SceneLoadManager.Instance.SceneLoad(ConstValues.eSceneName.Patch);
    }

}
