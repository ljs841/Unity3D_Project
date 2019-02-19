using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ConstValues;

/// <summary>
/// 씬 로딩이 끝난후에 해당씬에서 동적으로 생성되는 오브젝트들을 이곳에서 생성시킨다.
/// 해당 로딩 작업이 끝나기 전까지 씬 로딩완료처리가 되지 않는다.
/// </summary>
public class BattleSceneLoadObject  : SceneLoadObject
{
    Queue<BattleInfo> _loadQueue;
    List<BattleInfo> _currentBattleInfoList;
    WaitForSeconds _waitSecond = new WaitForSeconds(0.1f);
    GameObject map;
    int _count = 0;
    bool _bundleLodComplete;
    public override void ReadyLoadPrefab()
    {

        SceneManager.sceneLoaded += OnSceneLoadComplete;
        AssetBundleManager.Instance.OnComplete += BundleLoadComplete;
        _bundleLodComplete = false;
        _count = 0;
        if (_loadQueue == null)
        {
            _loadQueue = new Queue<BattleInfo>();
        }
        _loadQueue.Clear();
        AssetBundleManager.Instance.AssetBundleLoad(2);
        _currentBattleInfoList = BattleManager._Instance.GetBattleInfo();
        foreach (var obj in _currentBattleInfoList)
        {
            _loadQueue.Enqueue(obj);
        }
        _maxLoadCount = _loadQueue.Count;
    }

    void BundleLoadComplete()
    {
        _bundleLodComplete = true;
        AssetBundleManager.Instance.OnComplete -= BundleLoadComplete;
    }

    public override IEnumerator CorouctineLoadPrefab()
    {
        ReadyLoadPrefab();
        while (_bundleLodComplete == false)
        {
            yield return _waitSecond;
        }

        while (_loadQueue.Count != 0)
        {
            BattleInfo info = _loadQueue.Dequeue();
            if(info._assetType == eAssetTypeInGame.SFX)
            {
                AudioClip clip = AssetBundleManager.Instance.GetAsset<AudioClip>(info._bundleName, info._assetName);
                SoundManager.Instance.AddClip(info._assetName, clip);
            }
            else
            {
                GameObject obj = AssetBundleManager.Instance.GetAsset<GameObject>(info._bundleName, info._assetName);
                if (obj != null)
                {

                    GameObject sc = Instantiate(obj);

                    if (info._assetType == eAssetTypeInGame.Unit)
                    {
                        UnitObjectPool.Instance.AddResources(info._assetName, sc);
                        ChangeProgress(_count);
                    }
                    else if (info._assetType == eAssetTypeInGame.Map)
                    {
                        map = sc;
                    }
                    else
                    {
                        var fxScript = obj.GetComponent<FxObject>();
                        FxManager.Instance.AddResoruces(fxScript._type, sc);
                    }

                }
            }   
           
        }
        yield return null;
    }

    public override void OnSceneLoadComplete(Scene name, LoadSceneMode mode)
    {

        SceneManager.MoveGameObjectToScene(map, name);
        SceneManager.MoveGameObjectToScene(UnitObjectPool.Instance.gameObject, name);

        SceneManager.sceneLoaded -= OnSceneLoadComplete;
    }



    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}