using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.UI;

namespace UnityEngine.UI
{
    [CustomEditor(typeof(CustomButton))]
    public class CustomButtonEditor : ButtonEditor
    {
        SerializedProperty _onPointerDown;

        protected override void OnEnable()
        {
            base.OnEnable();
            _onPointerDown = serializedObject.FindProperty("m_OnPress");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(_onPointerDown);
            serializedObject.ApplyModifiedProperties();
        }
    }
}