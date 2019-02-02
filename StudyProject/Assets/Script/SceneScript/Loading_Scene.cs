using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValues;

public class Loading_Scene : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Loading_SceneStart");
    }
    // Use this for initialization
    void Start()
    {
        var sc = UIManager._Instance.CreateUIPrefab<Loading_Control>(ConstValue._loading, eUILayer.Layer1);
        sc.Show();
    }
}
