using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValues;

public class Battle_Scene : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Battle_SceneStart");
    }
    // Use this for initialization
    void Start()
    {
        var ff = BattleManager._Instance.GetPlayer();
        var sc = UIManager._Instance.CreateUIPrefab<Battle_Control>(ConstValue._battle, eUILayer.Layer1);
        sc.Show();
        ff.StartEntity();
    }
}
