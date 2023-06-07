using System;
using Core.Inventory_And_Item.Data;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Inventory_And_Item.Editor
{
    [CustomPropertyDrawer(typeof(EffectGeneratorAttribute))]
    public class EffectMakerAttributeDrawer : PropertyDrawer
    {
        private bool useResource;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EffectType typeEnum;
            Object     oldEffect = property.objectReferenceValue;
            Object     owner     = property.serializedObject.targetObject;

            useResource = EditorGUILayout.Toggle("Use Resource", useResource);
            if (useResource)
            {
                EditorGUILayout.PropertyField(property);
                return;
            }

            if (oldEffect == null)
            {
                typeEnum = EffectType.CreateNew;
            }
            else
            {
                if (!Enum.TryParse(oldEffect.GetType().Name, out typeEnum)) throw new Exception("Invalid effect type");
            }

            EffectType newType = (EffectType)EditorGUILayout.EnumPopup("Effect Type", typeEnum);
            if (typeEnum != newType)
            {
                typeEnum = newType;
                if (oldEffect != null) AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(oldEffect));

                if (newType == EffectType.CreateNew)
                {
                    AssetDatabase.SaveAssets();
                    property.objectReferenceValue = null;
                    EditorUtility.SetDirty(owner);
                }
                else
                {
                    ScriptableObject newEffect = ScriptableObject.CreateInstance(typeEnum.ToString());
                    newEffect.name = property.propertyPath.Replace(".Array.data", "") + "=" + typeEnum;
                    string ownerPath     = AssetDatabase.GetAssetPath(owner);
                    string newEffectPath = ownerPath.Replace(".asset", $".{newEffect.name}.asset");

                    AssetDatabase.CreateAsset(newEffect, newEffectPath);
                    AssetDatabase.SaveAssets();
                    property.objectReferenceValue = newEffect;
                    EditorUtility.SetDirty(newEffect);
                    EditorUtility.SetDirty(owner);
                }
            }
        }



        
    }
}