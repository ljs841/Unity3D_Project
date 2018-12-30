
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ConstValues;
public class SceneLoadManager : MonoBehaviour
{
    private static SceneLoadManager _instance;
    public static SceneLoadManager _Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("SceneLoadManager");
                _instance = obj.AddComponent<SceneLoadManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    bool _isSceneLoading = false;
    string _loadSceneName = string.Empty;
    WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
    WaitForSeconds waitSec= new WaitForSeconds(1.0f);
    private float _curProgress;
    public float _CurProgress
    {
        get
        {
            return _curProgress;
        }
    }
    public bool _IsSceneLoading
    {
        get
        {
            return _isSceneLoading;
        }
    }

    public void SceneLoad(string sceneName)
    {
        if(_isSceneLoading)
        {
            Util.DebugErrorLog("Already Scene Loading");
            return;
        }
        //새로운 씬을 불러오게 될 경우는 전투씬이거나 기존의 화면을 아얘 지운다고 가정
        //이전에 만들어진 UI프리팹은 일단 지운다.
        UIManager._Instance.ContentPopupAllDestroy();

        _loadSceneName = sceneName;
        SceneManager.LoadScene(SceneName.Loading.ToString());
        
    }
    public void StartLoad()
    {
        StartCoroutine(Loading(_loadSceneName));
    }


    IEnumerator Loading(string sceneName)
    {
        _isSceneLoading = true;
        AsyncOperation oper = SceneManager.LoadSceneAsync(sceneName);
        oper.allowSceneActivation = false;
        while (oper.isDone == false)
        {
            yield return null;
            _curProgress = oper.progress;

            Util.DebugLog(SceneLoadManager._Instance._CurProgress.ToString());
            yield return waitSec;
            if (oper.progress >= 0.9f)
            {
                _curProgress = 1f;
                yield return waitSec;
                _isSceneLoading = false;
                yield return waitFrame;
                oper.allowSceneActivation = true;



            }
        }
        
    }



}