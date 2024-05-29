using System;
using System.Collections.Generic;
using UnityEngine;
using HackathonTemplate.Utilities;

namespace HackathonTemplate.Editor
{
    /// <summary>
    /// �V�[���؂�ւ��̐ݒ�f�[�^
    /// </summary>
    [Serializable]
    public class SceneSwitcherConfigData
    {
        /// <summary>
        /// �V�[���t�H���_�̃p�X
        /// </summary>
        [Label("�V�[���t�H���_�̃p�X")]
        public string ScenesFolderPath = "Assets/HackathonTemplate/Scenes/";
        /// <summary>
        /// �C���Q�[���V�[���̖��O
        /// </summary>
        [Label("�C���Q�[���V�[���̖��O")]
        public string InGameSceneName = "InGame";
        /// <summary>
        /// �^�C�g���V�[���̖��O
        /// </summary>
        [Label("�^�C�g���V�[���̖��O")]
        public string TitleSceneName = "Title";
        /// <summary>
        /// ���U���g�V�[���̖��O
        /// </summary>
        [Label("���U���g�V�[���̖��O")]
        public string ResultSceneName = "Result";
    }

    /// <summary>
    /// �p�b�P�[�W�C���X�g�[���̐ݒ�f�[�^
    /// </summary>
    [Serializable]
    public class PackageInstallerConfigData
    {
        /// <summary>
        /// �����l
        /// </summary>
        private static Dictionary<string, string> DEFAULT_VALUE = new() {
            {"TextMeshPro", "com.unity.textmeshpro"},
            {"TMProDynamicDataCleaner", "https://github.com/STARasGAMES/tmpro-dynamic-data-cleaner.git#upm"},
        };

        /// <summary>
        /// �p�b�P�[�W�̎��ʎq
        /// </summary>
        [Label("�p�b�P�[�W�̎��ʎq")]
        public SerializableDictionary<string, string> _Identifiers = new(DEFAULT_VALUE);
    }

    /// <summary>
    /// �ݒ�X�N���v�^�u���I�u�W�F�N�g
    /// </summary>
    [CreateAssetMenu(menuName = "Hackathon/ScriptableObject/Config", fileName = "Config")]
    public class ConfigScriptableObject : ScriptableObject
    {
        /// <summary>
        /// �V�[���ؑւ̐ݒ�f�[�^
        /// </summary>
        [SerializeField, Header("�V�[���؂�ւ�")]
        private SceneSwitcherConfigData _menuSceneSwitcherConfigData = new SceneSwitcherConfigData();
        public SceneSwitcherConfigData MenuSceneSwitcherConfigData => _menuSceneSwitcherConfigData;

        /// <summary>
        /// �p�b�P�[�W�C���X�g�[���̐ݒ�f�[�^
        /// </summary>
        [SerializeField, Header("�p�b�P�[�W�C���X�g�[��")]
        private PackageInstallerConfigData _menuPackageInstallerConfigData = new PackageInstallerConfigData();
        public PackageInstallerConfigData PackageInstallerConfigData => _menuPackageInstallerConfigData;
    }
}

