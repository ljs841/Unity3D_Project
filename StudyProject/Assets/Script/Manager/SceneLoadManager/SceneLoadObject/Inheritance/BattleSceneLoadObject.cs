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
    Queue<string[]> _loadQueue;
    BattleInfo _currentBattleInfo;
    WaitForSeconds _waitSecond = new WaitForSeconds(0.5f);
    int _count = 0;
    bool _bundleLodComplete;
    public override void ReadyLoadPrefab()
    {
        AssetBundleManager.Instance.OnComplete += BundleLoadComplete;
        _bundleLodComplete = false;
        _count = 0;
        if (_loadQueue == null)
        {
            _loadQueue = new Queue<string[]>();
        }
        _loadQueue.Clear();
        AssetBundleManager.Instance.AssetBundleLoad(1);
        _currentBattleInfo = BattleManager._Instance.GetBattleInfo();
        foreach (var obj in _currentBattleInfo.unitQueue)
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
            string[] type = _loadQueue.Dequeue();
            GameObject obj = AssetBundleManager.Instance.GetAsset<GameObject>(type[0], type[1] );
            if(obj != null)
            {
                GameObject sc = Instantiate(obj);
                
                UnitObjectPool._Instance.AddResources(sc);
                ChangeProgress(_count);
            }
        }
    }

    public override void OnSceneLoadComplete(Scene name)
    {
        SceneManager.MoveGameObjectToScene(UnitObjectPool._Instance.gameObject, name);
    }



    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}