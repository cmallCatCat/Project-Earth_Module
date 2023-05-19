using System;
using Core.Inventory_And_Item.Data.ItemIdentifications.ItemEffects;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Inventory_And_Item.Data.ItemIdentifications.ItemFeatures
{
    [CustomPropertyDrawer(typeof(EffectGeneratorAttribute))]
    public class EffectMakerAttributeDrawer : PropertyDrawer
    {
        private EffectType typeEnum = EffectType.CreateNew;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.isArray)
            {
                int arraySize = property.arraySize;
                for (int i = 0; i < arraySize; i++)
                {
                    ShowOneEffect(position, property.GetArrayElementAtIndex(i), label);
                }
            }
            else
            {
                ShowOneEffect(position, property, label);
            }
        }

        private void ShowOneEffect(Rect position, SerializedProperty property, GUIContent label)
        {
            Object effect = property.objectReferenceValue;
            Object owner = property.serializedObject.targetObject;
            if (effect == null)
            {
                typeEnum = (EffectType)EditorGUI.EnumPopup(position, label, typeEnum);
                if (typeEnum != EffectType.CreateNew)
                {
                    ScriptableObject newEffect = ScriptableObject.CreateInstance(typeEnum.ToString());
                    newEffect.name = property.name + "=" + typeEnum;
                    string ownerPath = AssetDatabase.GetAssetPath(owner);
                    Debug.Log(AssetDatabase.Contains(owner));
                    string newEffectPath = ownerPath.Replace(".asset", $".{newEffect.name}.asset");
                    AssetDatabase.CreateAsset(newEffect, newEffectPath);
                    AssetDatabase.SaveAssets();
                    property.objectReferenceValue = newEffect;
                    EditorUtility.SetDirty(owner);
                    EditorUtility.SetDirty(newEffect);
                }
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}