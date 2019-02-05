using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MVVMTest))]
public class MVVMTestEditor : Editor
{
    private void OnEnable()
    {
        /*
        Model dd = new Model();
        Type ff = Type.GetType("Model");
        if(ff != null)
        {
            IEnumerable<PropertyInfo> pList = ff.GetProperties();
            IEnumerable<MethodInfo> mList = ff.GetMethods();
            foreach (PropertyInfo p in pList)
            {

                Debug.Log("\n" + p.DeclaringType.Name + ": " + p.Name);
            }

            foreach (MethodInfo m in mList)
            {
                Debug.Log("\n" + m.DeclaringType.Name + ": " + m.Name);
            }
        }
        */
    }
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
    }
}
