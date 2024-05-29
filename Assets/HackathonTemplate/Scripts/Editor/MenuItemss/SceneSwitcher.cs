using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace HackathonTemplate.Editor
{
    /// <summary>
    /// メニューからシーンを切り替えるクラス
    /// </summary>
    public class SceneSwitcher
    {
        /// <summary>
        /// メニューアイテムのパス
        /// </summary>
        private const string MENU_ITEM_PATH = "SceneSwitcher/";
        /// <summary>
        /// メニューアイテムの優先度
        /// </summary>
        private const int MENU_ITEM_PRIORITY = 1;
        /// <summary>
        /// インゲームシーンのアイテムの名前
        /// </summary>
        private const string INGAME_SCENE_ITEM_NAME = "InGame";
        /// <summary>
        /// インゲームシーンのアイテムの優先度
        /// </summary>
        private const int INGAME_SCENE_ITEM_PRIORITY = 1;
        /// <summary>
        /// タイトルシーンのアイテムの名前
        /// </summary>
        private const string TITLE_SCENE_ITEM_NAME = "Title";
        /// <summary>
        /// タイトルシーンのアイテムの優先度
        /// </summary>
        private const int TITLE_SCENE_ITEM_PRIORITY = 2;
        /// <summary>
        /// リザルトシーンのアイテムの名前
        /// </summary>
        private const string RESULT_SCENE_ITEM_NAME = "Result";
        /// <summary>
        /// リザルトシーンのアイテムの優先度
        /// </summary>
        private const int RESULT_SCENE_ITEM_PRIORITY = 3;
        /// <summary>
        /// メニューアイテムのパスとシーン名のディクショナリ
        /// </summary>
        private static Dictionary<string, string> _menuItemPathDictionary = new Dictionary<string, string>()
        {
            { MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH + INGAME_SCENE_ITEM_NAME, ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.InGameSceneName },
            { MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH + TITLE_SCENE_ITEM_NAME, ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.TitleSceneName },
            { MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH + RESULT_SCENE_ITEM_NAME, ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.ResultSceneName },
        };

        /// <summary>
        /// 初期化
        /// </summary>
        [InitializeOnLoadMethod]
        static void Initialize()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            SceneManager.sceneLoaded += OnSceneOpenedInPlaying;
            EditorSceneManager.sceneOpened += OnSceneOpenedInEditor;
            // エディタ起動時にsceneOpenedが呼ばれないため、初回のみdelayCallで実行
            EditorApplication.delayCall += OnSceneOpenedFirstTime;
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~SceneSwitcher ()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            SceneManager.sceneLoaded -= OnSceneOpenedInPlaying;
            EditorSceneManager.sceneOpened -= OnSceneOpenedInEditor;
            EditorApplication.delayCall -= OnSceneOpenedFirstTime;
        }

        /// <summary>
        /// シーン切替メニュー
        /// </summary>
        [MenuItem(MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH, priority = MENU_ITEM_PRIORITY)]
        private static void MenuItem() { }

        /// <summary>
        /// インゲームシーンを開く
        /// </summary>
        [MenuItem(MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH + INGAME_SCENE_ITEM_NAME, priority = INGAME_SCENE_ITEM_PRIORITY)]
        private static void OpenInGameScene()
        {
            OpenScene(ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.InGameSceneName);
        }

        /// <summary>
        /// タイトルシーンを開く
        /// </summary>
        [MenuItem(MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH + TITLE_SCENE_ITEM_NAME, priority = TITLE_SCENE_ITEM_PRIORITY)]
        private static void OpenTitleScene()
        {
            OpenScene(ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.TitleSceneName);
        }

        /// <summary>
        /// リザルトシーンを開く
        /// </summary>
        [MenuItem(MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH + RESULT_SCENE_ITEM_NAME, priority = RESULT_SCENE_ITEM_PRIORITY)]
        private static void OpenResultScene()
        {
            OpenScene(ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.ResultSceneName);
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            if(playModeStateChange == PlayModeStateChange.EnteredEditMode)
            {
                OnSceneOpenedFirstTime();
            }
        }

        /// <summary>
        /// シーンを開く
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        private static bool OpenScene(string sceneName)
        {
            try
            {
                // エディタが再生中か
                if (EditorApplication.isPlaying)
                {
                    SceneManager.LoadScene(sceneName);
                }
                else
                {
                    EditorSceneManager.OpenScene(ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.ScenesFolderPath + sceneName + ".unity");
                }
            }
            catch (ArgumentException)
            {
                Debug.LogError("シーンが見つかりません " + ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.ScenesFolderPath + sceneName + ".unity)");
                return false;
            }

            return true;
        }

        /// <summary>
        /// プレイ中にシーンが開かれたときの処理
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="mode">開くモード</param>
        private static void OnSceneOpenedInPlaying(Scene scene, LoadSceneMode mode)
        {
            SetMenuItemChecked(scene);
        }

        /// <summary>
        /// エディタでシーンが開かれたときの処理
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="mode">開くモード</param>
        private static void OnSceneOpenedInEditor(Scene scene, OpenSceneMode mode)
        {
            SetMenuItemChecked(scene);
        }

        /// <summary>
        /// シーンが最初に開かれたときの処理
        /// </summary>
        private static void OnSceneOpenedFirstTime()
        {
            SetMenuItemChecked(SceneManager.GetActiveScene());
        }

        /// <summary>
        /// メニューアイテムのチェックステータスをセット
        /// </summary>
        /// <param name="scene">シーン</param>
        private static void SetMenuItemChecked(Scene scene)
        {
            foreach (var path in _menuItemPathDictionary)
            {
                Menu.SetChecked(path.Key, SceneManager.GetSceneByPath(ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.ScenesFolderPath + path.Value + ".unity").name == scene.name);
            }
        }
    }
}