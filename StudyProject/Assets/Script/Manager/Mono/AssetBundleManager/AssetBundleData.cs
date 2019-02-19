using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleData
{
    //번들내에 로드한 에셋 리스트
    private List<UnityEngine.Object> _assetList;

    //에셋번들
    private AssetBundle _bundle;
    public AssetBundle Bundle
    {
        get
        {
            return _bundle;
        }
    }

    //에셋번들 네임 관리자 클래스에서 키값으로 사용
    private string _bundleName;
    public string BundleName
    {
        get
        {
            return _bundleName;
        }
    }

    //에셋번들을 로딩씬에서 로드 언로드 하기 위해 구분지은 열거형 
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
