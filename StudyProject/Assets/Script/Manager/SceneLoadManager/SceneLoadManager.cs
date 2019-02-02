using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ConstValues;
public class SceneLoadManager : SingleToneMono<SceneLoadManager>
{
    GameObject _sceneLoadObjet;
    Dictionary<eSceneName, SceneLoadObject> _dic;
    WaitForSeconds waitSec= new WaitForSeconds(0.1f);
    float _progress = 0.0f;

    public Action<float> OnProgressUpdate;
    public Action OnLoadingComplete;

    private void Awake()
    {
        gameObject.name = "SceneLoadManager";
        LoadObjectInit();
    }

    void LoadObjectInit()
    {
        _dic = new Dictionary<eSceneName, SceneLoadObject>();

        _sceneLoadObjet = new GameObject("_sceneLoadObjet");
        _sceneLoadObjet.transform.SetParent(this.transform);

        BattleSceneLoadObject obj = _sceneLoadObjet.AddComponent<BattleSceneLoadObject>();
        obj.SceneName = eSceneName.Battle;
        obj.LoadPrefabProgress += OnPrefabProgress;
        _dic.Add(eSceneName.Battle, obj);
    }
    
    public void SceneLoad(eSceneName sceneName)
    {
        //새로운 씬을 불러오게 될 경우는 전투씬이거나 기존의 화면을 아얘 지운다고 가정
        //이전에 만들어진 UI프리팹은 일단 지운다.
        UIManager._Instance.ContentPopupAllDestroy();
       
        StartCoroutine(LoadingSceneLoad(GetPrvSceneName() , sceneName));
    }


    eSceneName GetPrvSceneName()
    {
        return (eSceneName)SceneManager.GetActiveScene().buildIndex;
    }

    SceneLoadObject GetNextSceneLoadObject(eSceneName nextScene)
    {
        if (_dic.ContainsKey(nextScene))
        {
            return _dic[nextScene];
        }
        return null;
    }

    IEnumerator LoadingSceneLoad(eSceneName prvSceneName, eSceneName nextScene)
    {
        SceneLoadObject loadObj = GetNextSceneLoadObject(nextScene);

        yield return SceneManager.LoadSceneAsync(eSceneName.Loading.ToString(), LoadSceneMode.Additive);
        AssetBundleManager.Instance.DynamicBundleUnload();
        yield return SceneManager.UnloadSceneAsync(prvSceneName.ToString());

        yield return loadObj != null ? loadObj.CorouctineLoadPrefab() : null;

        ProgressUpdate(0.5f);

        SceneManager.sceneLoaded += OnSceneLoaded;
        AsyncOperation nextSceneOper = SceneManager.LoadSceneAsync(nextScene.ToString(), LoadSceneMode.Additive);
        nextSceneOper.completed += Oper_completed;
        while (nextSceneOper.isDone != true)
        {
            yield return waitSec;
            ProgressUpdate(0.5f + (nextSceneOper.progress * 0.5f));
        }

        yield return SceneManager.UnloadSceneAsync(eSceneName.Loading.ToString());
    }

    void ProgressUpdate(float progress)
    {
        _progress = progress;
        if(OnProgressUpdate != null)
        {
            OnProgressUpdate(_progress);
        }
    }

    void LoadComplete()
    {
        if(OnLoadingComplete != null)
        {
            OnLoadingComplete();
        }
    }

    void Oper_completed(AsyncOperation obj)
    {
      
    }

    void OnSceneLoaded(Scene name, LoadSceneMode mode)
    {
        LoadComplete();
        UIManager._Instance.SceneForCameraSetting();
        SceneManager.MoveGameObjectToScene(UnitObjectPool._Instance.gameObject, name);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnPrefabProgress(float progress)
    {
        
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}