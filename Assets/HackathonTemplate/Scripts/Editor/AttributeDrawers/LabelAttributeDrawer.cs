using UnityEngine;
using UnityEditor;
using HackathonTemplate.Utilities;

namespace HackathonTemplate.Editor
{
    /// <summary>
    /// インスペクター上で表示するラベルを指定する属性を描画
    /// </summary>
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelAttributeDrawer : PropertyDrawer
    {
        /// <summary>
        /// OnGUI
        /// </summary>
        /// <param name="position">position</param>
        /// <param name="property">property</param>
        /// <param name="label">label</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var newLabel = attribute as LabelAttribute;
            label = newLabel.Label;
            EditorGUI.PropertyField(position, property, label, true);
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