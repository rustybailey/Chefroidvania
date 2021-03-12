using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Inventory inventory = (Inventory)target;

        EditorGUILayout.LabelField("Acquire Ability");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Frying Pan"))
        {
            inventory.AcquireFryingPan();
        }

        if (GUILayout.Button("Knives"))
        {
            inventory.AcquireKnives();
        }

        if (GUILayout.Button("Tenderizer"))
        {
            inventory.AcquireTenderizer();
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("All Abilities"))
        {
            inventory.AcquireAllAbilities();
        }
    }
}
