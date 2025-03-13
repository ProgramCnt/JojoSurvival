using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    SerializedProperty itemName;
    SerializedProperty description;
    SerializedProperty icon;
    SerializedProperty dropPrefab;
    SerializedProperty type;
    SerializedProperty canStack;
    SerializedProperty maxStackAmount;

    SerializedProperty equipmentData;
    SerializedProperty consumableData;

    private void OnEnable()
    {
        itemName = serializedObject.FindProperty("itemName");
        description = serializedObject.FindProperty("description");
        icon = serializedObject.FindProperty("icon");
        dropPrefab = serializedObject.FindProperty("dropPrefab");
        type = serializedObject.FindProperty("type");
        canStack = serializedObject.FindProperty("canStack");
        maxStackAmount = serializedObject.FindProperty("maxStackAmount");
        equipmentData = serializedObject.FindProperty("equipmentData");
        consumableData = serializedObject.FindProperty("consumableData");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(itemName);
        EditorGUILayout.PropertyField(description);
        EditorGUILayout.PropertyField(icon);
        EditorGUILayout.PropertyField(dropPrefab);
        EditorGUILayout.PropertyField(type);

        EditorGUILayout.PropertyField(canStack);
        if (canStack.boolValue)
        {
            EditorGUILayout.PropertyField(maxStackAmount);
        }

        ItemType itemType = (ItemType)type.enumValueIndex;
        switch (itemType)
        {
            case ItemType.Equipment:
                EditorGUILayout.PropertyField(equipmentData);
                break;
            case ItemType.Consumable:
                EditorGUILayout.PropertyField(consumableData, true);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
