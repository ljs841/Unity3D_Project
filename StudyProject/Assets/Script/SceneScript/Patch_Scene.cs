using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ConstValues;
using UnityEngine.U2D;

public class Patch_Scene : MonoBehaviour
{

    private void Awake()
    {
        Debug.Log("Patch_SceneStart");
    }
    void Start()
    {
        AssetBundleManager.Instance.OnLoadComplete += OnLoadComplete;
        AssetBundleManager.Instance.OnComplete += OnComplete;
        AssetBundleManager.Instance.AssetBundleLoad(0);


    }

    void OnLoadComplete(string bundleName , string assetName)
    {
        
    }

    void OnComplete()
    {

        AssetBundleManager.Instance.OnLoadComplete -= OnLoadComplete;
        AssetBundleManager.Instance.OnComplete -= OnComplete;
        var sc = UIManager._Instance.CreateUIPrefab<Patch_Control>(ConstValue._patch, eUILayer.Layer1);
        sc.Show();
    }
}