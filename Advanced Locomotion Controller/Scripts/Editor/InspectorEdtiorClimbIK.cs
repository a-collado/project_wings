using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using FasTPS;

namespace FasTPS
{
    [CustomEditor(typeof(ClimbIK))]
    public class InspectorEdtiorClimbIK : Editor
    {
        public bool CustomizableSettings;
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            ClimbIK IK = (ClimbIK)target;

            EditorGUILayout.BeginHorizontal();
            Texture2D LogoTex = (Texture2D)Resources.Load("FasTPS_Logo_Gradient");
            GUILayout.Label(LogoTex, GUILayout.MaxWidth(135f), GUILayout.MaxHeight(25f));

            GUILayout.Label("Climb Behaviour", Title());
            EditorGUILayout.EndHorizontal();

            CustomizableSettings = GUILayout.Toggle(CustomizableSettings, "☼ Customizable", BoxStyle());
            CustomizableSettingsVars(IK);

            serializedObject.ApplyModifiedProperties();
        }
        void CustomizableSettingsVars(ClimbIK IK)
        {
            if (CustomizableSettings)
            {
                EditorGUILayout.LabelField("Variables", Text());
                serializedObject.FindProperty("lh").floatValue = EditorGUILayout.Slider("Left Hand IK", IK.lh, 0, 1);
                serializedObject.FindProperty("rh").floatValue = EditorGUILayout.Slider("Right Hand IK", IK.rh, 0, 1);
                serializedObject.FindProperty("lf").floatValue = EditorGUILayout.Slider("Left Foot IK", IK.lf, 0, 1);
                serializedObject.FindProperty("rf").floatValue = EditorGUILayout.Slider("Right Foot IK", IK.rf, 0, 1);
                serializedObject.FindProperty("helperSpeed").floatValue = EditorGUILayout.Slider("Helper Speed", IK.helperSpeed, 0, 100);
                serializedObject.FindProperty("forceFeetHeight").boolValue = EditorGUILayout.Toggle("Force Feet Height", IK.forceFeetHeight);
                EditorGUILayout.Space();
            }
        }
  
        public static GUIStyle BoxStyle()
        {
            GUIStyle style = new GUIStyle(EditorStyles.objectField);
            style.font = Resources.Load("TitilliumWeb-Bold") as Font;
            style.fontSize = 16;
            style.normal.textColor = Color.white;
            style.fontStyle = FontStyle.Normal;
            return style;
        }
        public static GUIStyle Title()
        {
            GUIStyle titleStyle = new GUIStyle(EditorStyles.label);
            titleStyle.font = Resources.Load("TitilliumWeb-Bold") as Font;
            titleStyle.fontSize = 18;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleLeft;
            return titleStyle;
        }
        public static GUIStyle Text()
        {
            GUIStyle titleStyle = new GUIStyle(EditorStyles.label);
            titleStyle.font = Resources.Load("TitilliumWeb-Bold") as Font;
            titleStyle.fontSize = 14;
            titleStyle.normal.textColor = Color.white;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleLeft;
            return titleStyle;
        }
    }
}
