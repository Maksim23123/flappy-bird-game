using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoosterButtonManager))]
public class BoostCountSetter : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BoosterButtonManager boosterButton = (BoosterButtonManager)target;

        boosterButton.countStorage.Value = EditorGUILayout.IntField("Count", boosterButton.countStorage.Value);
    }
}
