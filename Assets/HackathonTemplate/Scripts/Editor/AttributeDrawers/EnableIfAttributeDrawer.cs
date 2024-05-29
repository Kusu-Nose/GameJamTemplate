using UnityEditor;
using UnityEngine;
using HackathonTemplate.Utilities;

namespace HackathonTemplate.Editor
{
    /// <summary>
    /// bool型の変数がtrueの場合にプロパティを有効化する属性の描画
    /// </summary>
    [CustomPropertyDrawer(typeof(EnableIfAttribute))]
    internal sealed class EnableIfAttributeDrawer : PropertyDrawer
    {
        /// <summary>
        /// OnGUI
        /// </summary>
        /// <param name="position">position</param>
        /// <param name="property">property</param>
        /// <param name="label">label</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (EnableIfAttribute)attribute;
            var prop = property.serializedObject.FindProperty(attr.VariableName);

            if (prop == null)
            {
                Debug.LogError($"Property '{attr.VariableName}' not found");
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            EditorGUI.BeginDisabledGroup(!prop.boolValue);
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndDisabledGroup();
        }

        /// <summary>
        /// GetPropertyHeight
        /// </summary>
        /// <param name="property">property</param>
        /// <param name="label">label</param>
        /// <returns>PropertyHeight</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }
    }
}