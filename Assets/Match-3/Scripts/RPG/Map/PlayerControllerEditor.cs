using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;


[CustomEditor(typeof(PlayerController))]

public class PlayerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerController myScript = (PlayerController)target;

        if (GUILayout.Button("Reset"))
        {
            myScript.ResetPlayer();
        }

        if(GUILayout.Button("Set Data"))
        {
            myScript.SetPlayerData();
        }
    }
}
#endif