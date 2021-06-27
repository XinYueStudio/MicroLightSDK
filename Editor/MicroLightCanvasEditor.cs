//#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;


namespace MicroLight
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MicroLightCanvas))]
    public class MicroLightCanvasEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            MicroLightCanvas Manager = (MicroLightCanvas)target;
        
              Manager.UseCurvedUI = EditorGUILayout.Toggle("UseCurvedUI", Manager.UseCurvedUI, GUILayout.ExpandWidth(true));

            Manager.Angle = EditorGUILayout.Slider("Angle", Manager.Angle, -360,360, GUILayout.ExpandWidth(true));
            Manager.baseCircleSegments = (int)EditorGUILayout.Slider("Segments", Manager.baseCircleSegments, 1, 100, GUILayout.ExpandWidth(true));
            Manager.Quality = EditorGUILayout.Slider("Quality", Manager.Quality, 1, 10, GUILayout.ExpandWidth(true));

            Manager.DoUpdate();
            EditorGUILayout.Separator();
        }
        }
}
//#endif