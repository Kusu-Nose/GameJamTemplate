using UnityEngine;

namespace HackathonTemplate.Utilities
{
    /// <summary>
    /// �C���X�y�N�^�[��ŕ\�����郉�x�����w�肷�鑮��
    /// </summary>
    public class LabelAttribute : PropertyAttribute
    {
        /// <summary>
        /// ���x��
        /// </summary>
        public readonly GUIContent Label;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="label">���x��</param>
        public LabelAttribute(string label)
        {
            this.Label = new GUIContent(label);
        }
    }
}