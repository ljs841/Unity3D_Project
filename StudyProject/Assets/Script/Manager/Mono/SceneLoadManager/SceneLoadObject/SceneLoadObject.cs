using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ConstValues;

/// <summary>
/// 씬 로딩이 끝난후에 해당씬에서 동적으로 생성되는 오브젝트들을 이곳에서 생성시킨다.
/// 해당 로딩 작업이 끝나기 전까지 씬 로딩완료처리가 되지 않는다.
/// </summary>
public abstract class SceneLoadObject : MonoBehaviour
{
    protected eSceneName _sceneName;
    public eSceneName SceneName
    {
        get
        {
            return _sceneName;
        }
        set
        {
            _sceneName = value;
        }
    }
    protected bool _isPrefabLoadComplete = false;
    public bool IsPrefabLoadComplete
    {
        get
        {
            return true;
        }
    }

    protected int _maxLoadCount;
    protected float _progress;

    protected virtual void ChangeProgress(float completeCount)
    {
        _progress = completeCount/_maxLoadCount;
        if(LoadPrefabProgress != null)
        {
            LoadPrefabProgress(_progress);
        }
    }
    public Action<float> LoadPrefabProgress;

    //로드 되는 갯수값을 설정해주면 로딩 프로그레스에 정상적인 수치표시가능
    abstract public void ReadyLoadPrefab();
    //씬로딩 전에 객체를 먼저 로드하기위한 코루틴 함수
    abstract public IEnumerator CorouctineLoadPrefab();

    abstract public void OnSceneLoadComplete(Scene name, LoadSceneMode mode);


}