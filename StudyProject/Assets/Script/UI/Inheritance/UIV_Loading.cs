using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIV_Loading : UIComponent
{
    public Text _loadingMsg;
    public Slider _loadingSlider;
    
    WaitForSeconds waitForSec = new WaitForSeconds(0.1f);

    public override void Show()
    {
        base.Show();
        UpdateComponent(0);
        SceneLoadManager._Instance.StartLoad();
        StartCoroutine(LoadingProgressUpdate());

    }

    IEnumerator LoadingProgressUpdate()
    {
        while(SceneLoadManager._Instance._IsSceneLoading)
        {
            UpdateComponent(SceneLoadManager._Instance._CurProgress);
            yield return null;
            if (SceneLoadManager._Instance._IsSceneLoading == false)
            {
                StopCoroutine(LoadingProgressUpdate());
                PopupManager._Instance.DestroyQueueItem();
            }
           
        }

    }

    void UpdateComponent(float value)
    {
        _loadingSlider.value = value;
        _loadingMsg.text = string.Format("Progress... ({0})", value);
    }


}
