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
        if (GUILayout.Button("Acquire All Abilities"))
        {
            inventory.AcquireAllAbilities();
        }
    }
}
