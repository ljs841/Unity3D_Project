using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValues;

public class Login_Scene : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Login_SceneStart");
    }
    void Start()
    {
        UIManager._Instance.SceneForCameraSetting();
        var sc = UIManager._Instance.CreateUIPrefab<Login_Control>(ConstValue._login, eUILayer.Layer1);
        sc.Show();
    }
}
