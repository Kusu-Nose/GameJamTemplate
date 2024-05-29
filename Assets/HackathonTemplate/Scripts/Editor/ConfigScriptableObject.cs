using System;
using System.Collections.Generic;
using UnityEngine;
using HackathonTemplate.Utilities;

namespace HackathonTemplate.Editor
{
    /// <summary>
    /// シーン切り替えの設定データ
    /// </summary>
    [Serializable]
    public class SceneSwitcherConfigData
    {
        /// <summary>
        /// シーンフォルダのパス
        /// </summary>
        [Label("シーンフォルダのパス")]
        public string ScenesFolderPath = "Assets/HackathonTemplate/Scenes/";
        /// <summary>
        /// インゲームシーンの名前
        /// </summary>
        [Label("インゲームシーンの名前")]
        public string InGameSceneName = "InGame";
        /// <summary>
        /// タイトルシーンの名前
        /// </summary>
        [Label("タイトルシーンの名前")]
        public string TitleSceneName = "Title";
        /// <summary>
        /// リザルトシーンの名前
        /// </summary>
        [Label("リザルトシーンの名前")]
        public string ResultSceneName = "Result";
    }

    /// <summary>
    /// パッケージインストールの設定データ
    /// </summary>
    [Serializable]
    public class PackageInstallerConfigData
    {
        /// <summary>
        /// 初期値
        /// </summary>
        private static Dictionary<string, string> DEFAULT_VALUE = new() {
            {"TextMeshPro", "com.unity.textmeshpro"},
            {"TMProDynamicDataCleaner", "https://github.com/STARasGAMES/tmpro-dynamic-data-cleaner.git#upm"},
        };

        /// <summary>
        /// パッケージの識別子
        /// </summary>
        [Label("パッケージの識別子")]
        public SerializableDictionary<string, string> _Identifiers = new(DEFAULT_VALUE);
    }

    /// <summary>
    /// 設定スクリプタブルオブジェクト
    /// </summary>
    [CreateAssetMenu(menuName = "Hackathon/ScriptableObject/Config", fileName = "Config")]
    public class ConfigScriptableObject : ScriptableObject
    {
        /// <summary>
        /// シーン切替の設定データ
        /// </summary>
        [SerializeField, Header("シーン切り替え")]
        private SceneSwitcherConfigData _menuSceneSwitcherConfigData = new SceneSwitcherConfigData();
        public SceneSwitcherConfigData MenuSceneSwitcherConfigData => _menuSceneSwitcherConfigData;

        /// <summary>
        /// パッケージインストールの設定データ
        /// </summary>
        [SerializeField, Header("パッケージインストール")]
        private PackageInstallerConfigData _menuPackageInstallerConfigData = new PackageInstallerConfigData();
        public PackageInstallerConfigData PackageInstallerConfigData => _menuPackageInstallerConfigData;
    }
}

