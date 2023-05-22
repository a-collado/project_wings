﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using FasTPS;

namespace FasTPS
{
    [CustomEditor(typeof(ClimbEvents))]
    public class InspectorEdtiorClimbEvents : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            ClimbBehaviour behaviour = (ClimbBehaviour)target;

            EditorGUILayout.BeginHorizontal();
            Texture2D LogoTex = (Texture2D)Resources.Load("FasTPS_Logo_Gradient");
            GUILayout.Label(LogoTex, GUILayout.MaxWidth(135f), GUILayout.MaxHeight(25f));

            GUILayout.Label("Climb Events", Title());
            EditorGUILayout.EndHorizontal();
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
