using System.IO;
using UnityEditor;
using UnityEngine;

namespace HackathonTemplate.Editor
{
    /// <summary>
    /// 設定を読み込む
    /// </summary>
    [InitializeOnLoad]
    public class ConfigLoader
    {
        /// <summary>
        /// 設定用スクリプタブルオブジェクトのアセットのパス
        /// </summary>
        private const string CONFIG_ASSET_PATH = "Assets/HackathonTemplate/Config.asset";
        /// <summary>
        /// 設定スクリプタブルオブジェクト
        /// </summary>
        private static ConfigScriptableObject _config;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public static ConfigScriptableObject GetConfig()
        {
            try
            {
                if (null == _config)
                {
                    // ConfigScriptableObjectを読み込む
                    _config = AssetDatabase.LoadAssetAtPath<ConfigScriptableObject>(CONFIG_ASSET_PATH);
                }
                return _config;
            }
            catch (FileNotFoundException)
            {
                Debug.LogError("Config.asset が見つかりません");
                return null;
            }
        }
    }
}
