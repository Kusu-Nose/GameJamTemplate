using UnityEngine;

namespace HackathonTemplate.Utilities
{
    /// <summary>
    /// bool�^�̕ϐ���true�̏ꍇ�Ƀv���p�e�B��L�������鑮��
    /// </summary>
    public class EnableIfAttribute : PropertyAttribute
    {
        /// <summary>
        /// �ϐ���
        /// </summary>
        public readonly string VariableName;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="variableName">�ϐ���</param>
        public EnableIfAttribute(string variableName)
        {
            this.VariableName = variableName;
        }
    }
}