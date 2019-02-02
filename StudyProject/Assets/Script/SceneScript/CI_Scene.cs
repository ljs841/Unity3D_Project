using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValues;

public class CI_Scene : MonoBehaviour {
    private void Awake()
    {
        Debug.Log("CI_SceneStart");
    }

    // Use this for initialization
    void Start ()
    {
        var sc = UIManager._Instance.CreateUIPrefab<CI_Control>(ConstValue._cI , eUILayer.Layer1);
        sc.Show();
    }
}
