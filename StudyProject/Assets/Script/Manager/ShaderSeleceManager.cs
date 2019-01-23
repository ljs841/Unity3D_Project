using System;
using System.Collections.Generic;
using UnityEngine;
using ConstValues;

public class ShaderSeleceManager : MonoBehaviour
{
    private static ShaderSeleceManager _instance;
    public static ShaderSeleceManager _Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("ShaderSeleceManager");
                _instance = obj.AddComponent<ShaderSeleceManager>();
                _instance.Init();
            }
            return _instance;
        }
    }

    Dictionary<string, Material> _shaderDic;

    public void Init()
    {
        _shaderDic = new Dictionary<string, Material>();
       
    }

    Material CreateShaderMaterial(string shaderName)
    {
        Material mat = new Material(Shader.Find(shaderName));
        _shaderDic.Add(shaderName, mat);
        return mat;
    }

    public Material FindMaterial(string shaderName)
    {
        if(_shaderDic.ContainsKey(shaderName))
        {
            return _shaderDic[shaderName];
        }
       return CreateShaderMaterial(shaderName);

    }
}