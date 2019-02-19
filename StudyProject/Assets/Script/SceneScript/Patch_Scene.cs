using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ConstValues;
using UnityEngine.U2D;

public class Patch_Scene : MonoBehaviour
{

    void Start()
    {
        UIManager._Instance.SceneForCameraSetting();
        var sc = UIManager._Instance.CreateUIPrefab<Patch_Control>(ConstValue._patch, eUILayer.Layer1);
        sc.Show();
        AssetBundleManager.Instance.OnLoadComplete += OnLoadComplete;
        AssetBundleManager.Instance.OnComplete += OnComplete;
        AssetBundleManager.Instance.AssetBundleLoad(0);
    }

    void OnLoadComplete(string bundleName , string assetName)
    {
        //에셋 번들내에 오브젝트가 로드될때마다 번들네임과 에셋이름을 콜백으로 받는다.
    }

    void OnComplete()
    {
        //지정한 인덱스의 에셋이 전부 로드가 끝나면 호출
        //대리함수 연결 해제
        AssetBundleManager.Instance.OnLoadComplete -= OnLoadComplete;
        AssetBundleManager.Instance.OnComplete -= OnComplete;
      
    }
}