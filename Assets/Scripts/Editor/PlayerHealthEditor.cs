using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerHealth))]
public class PlayerHealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerHealth playerHealth = (PlayerHealth)target;
        if (GUILayout.Button("Full Heal"))
        {
            playerHealth.FullHeal();
        }

        if (GUILayout.Button("Increase Max Health"))
        {
            playerHealth.HandleIncreasingMaxHealth();
        }

        if (GUILayout.Button("Damage Player"))
        {
            playerHealth.DecreaseHealth();
        }
    }
}
