using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading_View : UIView
{
    public Text _loadingMsg;
    public Slider _loadingSlider;
    
    public override void Show()
    {
        base.Show();
        UpdateComponent(0);
        SceneLoadManager.Instance.OnProgressUpdate += UpdateComponent;
        SceneLoadManager.Instance.OnLoadingComplete += LoadingComplete;

    }

    void UpdateComponent(float value)
    {
        _loadingSlider.value = value;
        _loadingMsg.text = string.Format("Progress... ({0})", value);
    }
    
    void LoadingComplete()
    {
        SceneLoadManager.Instance.OnProgressUpdate -= UpdateComponent;
        SceneLoadManager.Instance.OnLoadingComplete -= LoadingComplete;
        PopupManager._Instance.DestroyQueueItem();

    }


}
