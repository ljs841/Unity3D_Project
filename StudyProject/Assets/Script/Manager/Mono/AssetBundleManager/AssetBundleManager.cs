using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AssetBundleManager : SingleToneMono<AssetBundleManager>
{
   
    //로드한 에셋번들을 가지고있는 컬렉션
    Dictionary<string, AssetBundleData> _bundleDIc;
    //비동기 로드를 위한 큐
    Queue<AsssetBundleLoadTable> _loadQueue;

    AssetBundleData _currentBundle;
    //로드가 완료 될때 알려주기위한 대리함수
    public Action<string,string> OnLoadComplete;
    public Action OnComplete;

    private void Awake()
    {
        gameObject.name = "AssetBundleManager";
        _loadQueue = new Queue<AsssetBundleLoadTable>();
        _bundleDIc = new Dictionary<string, AssetBundleData>();
        SpriteAtlasManager.atlasRequested += OnSpriteAtlasRequest;
    }

    public void AssetBundleLoad(int loadIndex , bool isAsync = true)
    {
        _loadQueue.Clear();
        var list = TempData.GetBundleLoadTable(loadIndex);
        foreach(var ob in list)
        {
            _loadQueue.Enqueue(ob);            
        }

        StartCoroutine(isAsync ?  coLoadBundle() : coLoadBundle());
    }

    IEnumerator coLoadBundle()
    {
        while(_loadQueue.Count != 0)
        {
            AsssetBundleLoadTable bundleTableInfo = _loadQueue.Dequeue();
            if (_bundleDIc.ContainsKey(bundleTableInfo._bundleName))
                continue;

            AssetBundleCreateRequest requestBundle = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, bundleTableInfo._bundleName));
            yield return requestBundle;
            _currentBundle = AddAseetBundle(bundleTableInfo._bundleName, requestBundle.assetBundle , bundleTableInfo._bundleType);

            AssetBundleRequest assetRequest = requestBundle.assetBundle.LoadAllAssetsAsync();
            assetRequest.completed += AssetRequest_completed;
            yield return assetRequest;

          
        }
        Complete();
        yield return null;
    }

    private void AssetRequest_completed(AsyncOperation obj)
    {
        Debug.Log("load asset");
        var assetRequest = (AssetBundleRequest)obj;
        for (int i = 0; i < assetRequest.allAssets.Length; i++)
        {
            _currentBundle.AddAsset(assetRequest.allAssets[i].name, assetRequest.allAssets[i]);
            LoadComplete(_currentBundle.BundleName, assetRequest.allAssets[i].name);
        }
        assetRequest.completed -= AssetRequest_completed;
    }

    AssetBundleData AddAseetBundle(string bundleName , AssetBundle bundle , eBundleLoadType loadType)
    {
        AssetBundleData data = null;
        if (_bundleDIc.ContainsKey(bundleName) == false)
        {
            data = CreateBundleData(bundleName, bundle , loadType);
            _bundleDIc.Add(bundleName, data);
        }
        return data;
    }

    public T GetAsset<T>(string bundleName, string assetName) where T : UnityEngine.Object
    {
        return (T)GetAsset(bundleName, assetName, typeof(T));
    }



    public UnityEngine.Object GetAsset(string bundleName , string assetName , Type type = null)
    {
        if(bundleName == string.Empty)
        {
            foreach (AssetBundleData value in _bundleDIc.Values)
            {
                var ob = value.GetAsset(assetName, type);
                if (ob != null)
                {
                    return ob;
                }
                else
                {
                    continue;
                }
            }
        }
        else
        {
            if (_bundleDIc.ContainsKey(bundleName))
            {
                return _bundleDIc[bundleName].GetAsset(assetName, type);
            }
            Util.DebugErrorLog("Not Find Asset or The Asset has not been loaded yet.");
        }
       
        return null;
    }

    public void UnLoadAssetBundle(string bundleName)
    {
        if(_bundleDIc.ContainsKey(bundleName))
        {
            _bundleDIc[bundleName].UnLoadAssetBundle();
            _bundleDIc.Remove(bundleName);
        }
    }


    void LoadComplete(string bundleName,string assetName)
    {
        OnLoadComplete?.Invoke(bundleName, assetName);
    }

    void Complete()
    {
        StopAllCoroutines();
        OnComplete?.Invoke();
    }

    void OnSpriteAtlasRequest(string info, Action<SpriteAtlas> callBack)
    {
        Debug.Log("request spriteatlas " + info);
        SpriteAtlas data = GetAsset<SpriteAtlas>(string.Empty, info);
        if (data != null)
        {
            callBack.Invoke(data);
        }
    }

    public void DynamicBundleUnload()
    {
        List<AssetBundleData> _list = new List<AssetBundleData>();
        foreach (var ob in _bundleDIc.Values)
        {
            if(ob.LoadType == eBundleLoadType.Dynamic)
            {
                _list.Add(ob);
                ob.UnLoadAssetBundle();
            }
        }
        foreach(var ob in _list)
        {
            _bundleDIc.Remove(ob.BundleName);
        }
    }

    AssetBundleData CreateBundleData(string bundleName, AssetBundle bundle , eBundleLoadType loadtype)
    {
        AssetBundleData bundleData = new AssetBundleData(bundleName, bundle , loadtype);
        return bundleData;
    }

    private void OnDestroy()
    {
        foreach (var ob in _bundleDIc.Values)
        {
            ob.UnLoadAssetBundle();
        }
        _bundleDIc.Clear();
        AssetBundle.UnloadAllAssetBundles(true);

    }
}

