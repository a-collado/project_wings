using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using FasTPS;

namespace FasTPS
{
    [CustomEditor(typeof(ClimbBehaviour))]
    public class InspectorEdtiorClimbBehaviour : Editor
    {
        public bool VariableSettings, CurveSettings, CustomizableSettings;
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            ClimbBehaviour behaviour = (ClimbBehaviour)target;

            EditorGUILayout.BeginHorizontal();
            Texture2D LogoTex = (Texture2D)Resources.Load("FasTPS_Logo_Gradient");
            GUILayout.Label(LogoTex, GUILayout.MaxWidth(135f), GUILayout.MaxHeight(25f));

            GUILayout.Label("Climb Behaviour", Title());
            EditorGUILayout.EndHorizontal();

            VariableSettings = GUILayout.Toggle(VariableSettings, "⊕ Variables", BoxStyle());
            VariableSettingsVars(behaviour);

            CurveSettings = GUILayout.Toggle(CurveSettings, "∿  Curves", BoxStyle());
            CurveSettingsVars(behaviour);

            CustomizableSettings = GUILayout.Toggle(CustomizableSettings, "☼ Customizable", BoxStyle());
            CustomizableSettingsVars(behaviour);

            serializedObject.ApplyModifiedProperties();
        }
        void VariableSettingsVars(ClimbBehaviour behaviour)
        {
            if (VariableSettings)
            {
                EditorGUILayout.LabelField("Variables", Text());
                serializedObject.FindProperty("rootOffset").vector3Value = EditorGUILayout.Vector3Field("Walk Speed", behaviour.rootOffset);
                serializedObject.FindProperty("speed_linear").floatValue = EditorGUILayout.Slider("Linear Speed", behaviour.speed_linear, 0, 5);
                serializedObject.FindProperty("speed_direct").floatValue = EditorGUILayout.Slider("Direct Speed", behaviour.speed_direct, 0, 5);
                serializedObject.FindProperty("speed_dropLedge").floatValue = EditorGUILayout.Slider("DropLedge Speed", behaviour.speed_dropLedge, 0, 5);
                EditorGUILayout.Space();
            }
        }
        void CurveSettingsVars(ClimbBehaviour behaviour)
        {
            if (CurveSettings)
            {
                EditorGUILayout.LabelField("Curves", Text());
                serializedObject.FindProperty("a_jumpingCurve").animationCurveValue = EditorGUILayout.CurveField("Jumping Curve", behaviour.a_jumpingCurve);
                serializedObject.FindProperty("a_mountCurve").animationCurveValue = EditorGUILayout.CurveField("Mount Curve", behaviour.a_mountCurve);
                serializedObject.FindProperty("a_zeroToOne").animationCurveValue = EditorGUILayout.CurveField("ZeroToOne Curve", behaviour.a_zeroToOne);
                EditorGUILayout.Space();
            }
        }
        void CustomizableSettingsVars(ClimbBehaviour behaviour)
        {
            if (CustomizableSettings)
            {
                EditorGUILayout.LabelField("Customizable", Text());
                serializedObject.FindProperty("enableRootMovement").boolValue = EditorGUILayout.Toggle("Enable Root Movement", behaviour.enableRootMovement);
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
