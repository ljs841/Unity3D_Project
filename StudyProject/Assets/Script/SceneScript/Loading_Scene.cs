using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValues;

public class Loading_Scene : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var sc = UIManager._Instance.CreateUIPrefab<UIC_Loading>(ConstValue._loading, eUILayer.Layer1);
        sc.Show();
    }
}
