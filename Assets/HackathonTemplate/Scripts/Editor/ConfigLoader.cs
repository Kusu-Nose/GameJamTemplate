using System.IO;
using UnityEditor;
using UnityEngine;

namespace HackathonTemplate.Editor
{
    /// <summary>
    /// �ݒ��ǂݍ���
    /// </summary>
    [InitializeOnLoad]
    public class ConfigLoader
    {
        /// <summary>
        /// �ݒ�p�X�N���v�^�u���I�u�W�F�N�g�̃A�Z�b�g�̃p�X
        /// </summary>
        private const string CONFIG_ASSET_PATH = "Assets/HackathonTemplate/Config.asset";
        /// <summary>
        /// �ݒ�X�N���v�^�u���I�u�W�F�N�g
        /// </summary>
        private static ConfigScriptableObject _config;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public static ConfigScriptableObject GetConfig()
        {
            try
            {
                if (null == _config)
                {
                    // ConfigScriptableObject��ǂݍ���
                    _config = AssetDatabase.LoadAssetAtPath<ConfigScriptableObject>(CONFIG_ASSET_PATH);
                }
                return _config;
            }
            catch (FileNotFoundException)
            {
                Debug.LogError("Config.asset ��������܂���");
                return null;
            }
        }
    }
}
