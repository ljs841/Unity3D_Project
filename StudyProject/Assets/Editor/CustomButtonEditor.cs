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
        SerializedProperty _onPointerUp;

        protected override void OnEnable()
        {
            base.OnEnable();
            _onPointerDown = serializedObject.FindProperty("m_OnPress");
            _onPointerUp = serializedObject.FindProperty("m_OnRelease");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUILayout.PropertyField(_onPointerDown);
            EditorGUILayout.PropertyField(_onPointerUp);
            serializedObject.ApplyModifiedProperties();
        }
    }
}