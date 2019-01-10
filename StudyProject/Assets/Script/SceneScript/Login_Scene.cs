using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValues;

public class Login_Scene : MonoBehaviour
{ 
    void Start()
    {
        var sc = UIManager._Instance.CreateUIPrefab<UIC_Login>(ConstValue._login, eUILayer.Layer1);
        sc.Show();
    }
}
