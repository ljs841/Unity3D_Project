using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Patch_Control : UIController
{
    public Sprite dd;
    public override void Create()
    {
        base.Create();

        StartCoroutine(coDealy());
      
    }


    IEnumerator coDealy()
    {
        yield return new WaitForSeconds(2);
        SceneLoadManager.Instance.SceneLoad(ConstValues.eSceneName.Login);
    }
}