using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace HackathonTemplate.Editor
{
    /// <summary>
    /// パッケージをインストール
    /// </summary>
    public class PackageInstaller
    {
        /// <summary>
        /// リクエスト
        /// </summary>
        private static AddAndRemoveRequest _request;

        /// <summary>
        /// パッケージをインストール
        /// </summary>
        public static void InstallPackages()
        {
            // ダイアログ表示
            bool result = EditorUtility.DisplayDialog(
                "パッケージのインストール",
                "以下のパッケージをインストールしますか？\n\n" +
                ConfigLoader.GetConfig().PackageInstallerConfigData._Identifiers.Keys.Aggregate((x, y) => x + "\n" + y),
                "Yes",
                "No");

            if (result)
            {
                Debug.Log("パッケージのインストール中...");

                List<string> identifiers = new(ConfigLoader.GetConfig().PackageInstallerConfigData._Identifiers.Values);
                // インストール
                _request = Client.AddAndRemove(identifiers.ToArray());
                EditorApplication.update += Progress;
            }
        }

        /// <summary>
        /// 進捗
        /// </summary>
        static void Progress()
        {
            if (_request.IsCompleted)
            {
                if (_request.Status == StatusCode.Success)
                {
                    Debug.Log("パッケージのインストール完了");
                }
                else if (_request.Status >= StatusCode.Failure)
                {
                    Debug.Log(_request.Error.message);
                }
                EditorApplication.update -= Progress;
            }
        }
    }
}