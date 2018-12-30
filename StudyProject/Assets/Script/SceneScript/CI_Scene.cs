using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValues.UIResPath;

public class CI_Scene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        var sc = UIManager._Instance.CreateUIPrefab<UIC_CI>(ConstValue._cI , eUILayer.Layer1);
        sc.Show();
    }
}
