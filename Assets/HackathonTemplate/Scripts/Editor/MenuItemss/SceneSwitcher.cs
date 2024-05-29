using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace HackathonTemplate.Editor
{
    /// <summary>
    /// ���j���[����V�[����؂�ւ���N���X
    /// </summary>
    public class SceneSwitcher
    {
        /// <summary>
        /// ���j���[�A�C�e���̃p�X
        /// </summary>
        private const string MENU_ITEM_PATH = "SceneSwitcher/";
        /// <summary>
        /// ���j���[�A�C�e���̗D��x
        /// </summary>
        private const int MENU_ITEM_PRIORITY = 1;
        /// <summary>
        /// �C���Q�[���V�[���̃A�C�e���̖��O
        /// </summary>
        private const string INGAME_SCENE_ITEM_NAME = "InGame";
        /// <summary>
        /// �C���Q�[���V�[���̃A�C�e���̗D��x
        /// </summary>
        private const int INGAME_SCENE_ITEM_PRIORITY = 1;
        /// <summary>
        /// �^�C�g���V�[���̃A�C�e���̖��O
        /// </summary>
        private const string TITLE_SCENE_ITEM_NAME = "Title";
        /// <summary>
        /// �^�C�g���V�[���̃A�C�e���̗D��x
        /// </summary>
        private const int TITLE_SCENE_ITEM_PRIORITY = 2;
        /// <summary>
        /// ���U���g�V�[���̃A�C�e���̖��O
        /// </summary>
        private const string RESULT_SCENE_ITEM_NAME = "Result";
        /// <summary>
        /// ���U���g�V�[���̃A�C�e���̗D��x
        /// </summary>
        private const int RESULT_SCENE_ITEM_PRIORITY = 3;
        /// <summary>
        /// ���j���[�A�C�e���̃p�X�ƃV�[�����̃f�B�N�V���i��
        /// </summary>
        private static Dictionary<string, string> _menuItemPathDictionary = new Dictionary<string, string>()
        {
            { MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH + INGAME_SCENE_ITEM_NAME, ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.InGameSceneName },
            { MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH + TITLE_SCENE_ITEM_NAME, ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.TitleSceneName },
            { MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH + RESULT_SCENE_ITEM_NAME, ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.ResultSceneName },
        };

        /// <summary>
        /// ������
        /// </summary>
        [InitializeOnLoadMethod]
        static void Initialize()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            SceneManager.sceneLoaded += OnSceneOpenedInPlaying;
            EditorSceneManager.sceneOpened += OnSceneOpenedInEditor;
            // �G�f�B�^�N������sceneOpened���Ă΂�Ȃ����߁A����̂�delayCall�Ŏ��s
            EditorApplication.delayCall += OnSceneOpenedFirstTime;
        }

        /// <summary>
        /// �f�X�g���N�^
        /// </summary>
        ~SceneSwitcher ()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            SceneManager.sceneLoaded -= OnSceneOpenedInPlaying;
            EditorSceneManager.sceneOpened -= OnSceneOpenedInEditor;
            EditorApplication.delayCall -= OnSceneOpenedFirstTime;
        }

        /// <summary>
        /// �V�[���ؑփ��j���[
        /// </summary>
        [MenuItem(MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH, priority = MENU_ITEM_PRIORITY)]
        private static void MenuItem() { }

        /// <summary>
        /// �C���Q�[���V�[�����J��
        /// </summary>
        [MenuItem(MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH + INGAME_SCENE_ITEM_NAME, priority = INGAME_SCENE_ITEM_PRIORITY)]
        private static void OpenInGameScene()
        {
            OpenScene(ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.InGameSceneName);
        }

        /// <summary>
        /// �^�C�g���V�[�����J��
        /// </summary>
        [MenuItem(MenuConfig.TOP_MENU_PATH + MENU_ITEM_PATH + TITLE_SCENE_ITEM_NAME, priority = TITLE_SCENE_ITEM_PRIORITY)]
        private static void OpenTitleScene()
        {
            OpenScene(ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.TitleSceneName);
        }

        /// <summary>
        /// ���U���g�V�[�����J��
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
        /// �V�[�����J��
        /// </summary>
        /// <param name="sceneName">�V�[����</param>
        private static bool OpenScene(string sceneName)
        {
            try
            {
                // �G�f�B�^���Đ�����
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
                Debug.LogError("�V�[����������܂��� " + ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.ScenesFolderPath + sceneName + ".unity)");
                return false;
            }

            return true;
        }

        /// <summary>
        /// �v���C���ɃV�[�����J���ꂽ�Ƃ��̏���
        /// </summary>
        /// <param name="scene">�V�[��</param>
        /// <param name="mode">�J�����[�h</param>
        private static void OnSceneOpenedInPlaying(Scene scene, LoadSceneMode mode)
        {
            SetMenuItemChecked(scene);
        }

        /// <summary>
        /// �G�f�B�^�ŃV�[�����J���ꂽ�Ƃ��̏���
        /// </summary>
        /// <param name="scene">�V�[��</param>
        /// <param name="mode">�J�����[�h</param>
        private static void OnSceneOpenedInEditor(Scene scene, OpenSceneMode mode)
        {
            SetMenuItemChecked(scene);
        }

        /// <summary>
        /// �V�[�����ŏ��ɊJ���ꂽ�Ƃ��̏���
        /// </summary>
        private static void OnSceneOpenedFirstTime()
        {
            SetMenuItemChecked(SceneManager.GetActiveScene());
        }

        /// <summary>
        /// ���j���[�A�C�e���̃`�F�b�N�X�e�[�^�X���Z�b�g
        /// </summary>
        /// <param name="scene">�V�[��</param>
        private static void SetMenuItemChecked(Scene scene)
        {
            foreach (var path in _menuItemPathDictionary)
            {
                Menu.SetChecked(path.Key, SceneManager.GetSceneByPath(ConfigLoader.GetConfig().MenuSceneSwitcherConfigData.ScenesFolderPath + path.Value + ".unity").name == scene.name);
            }
        }
    }
}