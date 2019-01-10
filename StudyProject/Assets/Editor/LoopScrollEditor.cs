using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.UI;
[CustomEditor (typeof(LoopScroll))  ]
public class LoopScrollEditor : ScrollRectEditor
{
    LoopScroll mp;
    protected override void OnEnable()
    {
        base.OnEnable();
        mp = (LoopScroll)target; 
    }
    public override void OnInspectorGUI()
    {
        mp._ItemGrid = (ScrollViewLoopGrid)EditorGUILayout.ObjectField("ItemGridScript",mp._ItemGrid, typeof(ScrollViewLoopGrid) ,true );
        base.OnInspectorGUI();
    }
}
