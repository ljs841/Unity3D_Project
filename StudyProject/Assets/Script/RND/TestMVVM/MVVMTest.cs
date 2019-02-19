using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class MVVMTest : MonoBehaviour
{
    ViewModel _model;
    public List<ComponenetBinder> _bindingInfo;
    private void Awake()
    {
        _model = new ViewModel();

        if (_bindingInfo == null)
        {
            _bindingInfo = new List<ComponenetBinder>();
        }
        Type type = _model.GetType();
        foreach (ComponenetBinder binder in _bindingInfo)
        {
            PropertyInfo p = type.GetProperty(binder.PropertName);
            Debug.Log(p);
        }
  
      
   

    }
}