using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstValues.UIResPath;

public class CI_Scene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        var sc = UIManager._Instance.CreateUIPrefab<CI_Controller>(ConstValue._uiRes_CI , eUILayer.Layer1);
        sc.Init<CI_Component>(sc.gameObject);
        sc.Show();
    }
}
