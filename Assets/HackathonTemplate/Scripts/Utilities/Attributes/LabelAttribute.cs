using UnityEngine;

namespace HackathonTemplate.Utilities
{
    /// <summary>
    /// インスペクター上で表示するラベルを指定する属性
    /// </summary>
    public class LabelAttribute : PropertyAttribute
    {
        /// <summary>
        /// ラベル
        /// </summary>
        public readonly GUIContent Label;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="label">ラベル</param>
        public LabelAttribute(string label)
        {
            this.Label = new GUIContent(label);
        }
    }
}