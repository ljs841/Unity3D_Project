using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleData
{
    private List<UnityEngine.Object> _assetList;
    private AssetBundle _bundle;
    public AssetBundle Bundle
    {
        get
        {
            return _bundle;
        }
    }
    private string _bundleName;
    public string BundleName
    {
        get
        {
            return _bundleName;
        }
    }

    private eBundleLoadType _loadType;
    public eBundleLoadType LoadType
    {
        get
        {
            return _loadType;
        }

        set
        {
            _loadType = value;
        }
    }

    public IEnumerator coAllLoadAsset()
    {
        AssetBundleRequest result = _bundle.LoadAllAssetsAsync();
        yield return result;

        foreach (UnityEngine.Object obj in result.allAssets)
        {
            AddAsset(obj.name, obj);
        }
    }

    public IEnumerator LoadAsset(string assetName, Action<UnityEngine.Object> onComplete = null) 
    {
        AssetBundleRequest asset = _bundle.LoadAssetAsync(assetName);
        yield return asset;
        AddAsset(assetName, asset.asset);
    }


    public AssetBundleData(string bundleName, AssetBundle bundle , eBundleLoadType loadType)
    {
        _bundle = bundle;
        _bundleName = bundleName;
        _loadType = loadType;
        _assetList = new List<UnityEngine.Object>();
    }

    public void AddAsset(string assetname, UnityEngine.Object asset)
    {
        _assetList.Add(asset);
    }


    public UnityEngine.Object GetAsset(string assetName , Type type = null)
    {
        /*
        if (IsIncludeAssetBundle(assetName))
        {
            return null;
        }
        */
        foreach(UnityEngine.Object asset in _assetList)
        {
            if(assetName.Equals(asset.name))
            {
                if(type == null)
                {
                    return asset;
                }
                else if(type.Equals(asset.GetType()))
                {
                    return asset;
                }
            }
        }
        return null;
    }

    bool IsStaticTypeBundle()
    {
        return _loadType == eBundleLoadType.Static;
    }

    public void UnLoadAssetBundle()
    {
        _assetList.Clear();
        _bundle.Unload(true);

    }

    /// <summary>
    /// StreamingAsset is null
    /// </summary>
    /// <param name="assetName"></param>
    /// <returns></returns>
    bool IsIncludeAssetBundle(string assetName)
    {
        foreach (string name in Bundle.GetAllAssetNames())
        {
            if (name.Equals(assetName))
            {
                return true;
            }
        }
        Util.DebugErrorLog("Not Include Asset");
        return false;
    }


}
