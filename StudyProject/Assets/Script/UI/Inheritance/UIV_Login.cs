using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ConstValues;
public class UIV_Login : UIComponent 
{
    public GameObject _newUser;
    public GameObject _alreadyLoggedIn;


    string _idStr = string.Empty;
    string _passWordStr = string.Empty;

    public void OnChanggeValue_ID(string value)
    {
        _idStr = value;
    }

    public void OnChangeValue_Password(string value)
    {
        _passWordStr = value;
    }

    public void OnEndEidt_ID(string value)
    {
        _idStr = value;
    }

    public void OnEndEidt_Password(string value)
    {
        _passWordStr = value;
    }

    public void OnClickScrollList()
    {
        var sc = UIManager._Instance.CreateUIPrefab<UIC_ScrollTest>(ConstValue._scrolTest, eUILayer.Layer1);
        sc.Show();
    }

}
