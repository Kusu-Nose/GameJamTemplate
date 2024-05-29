using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using HackathonTemplate.UIs;

namespace HackathonTemplate.Editor
{
    public partial class SampleGameManagerWindow : EditorWindow
    {
        /// <summary>
        /// ���j���[�A�C�e���̖��O
        /// </summary>
        private const string MENU_ITEM_NAME = "SampleGameManager";
        /// <summary>
        /// ���j���[�A�C�e���̗D��x
        /// </summary>
        private const int MENU_ITEM_PRIORITY = 0;
        /// <summary>
        /// �X�R�A�����l
        /// </summary>
        private static readonly int[] SCORE_MODIFIER_VALUES = new[]{ 1, 10, 100, 1000 };

        /// <summary>
        /// �E�B���h�E�\��
        /// </summary>
        [MenuItem(MenuConfig.TOP_MENU_PATH + MENU_ITEM_NAME, priority = MENU_ITEM_PRIORITY)]
        private static void ShowWindow()
        {
            var window = GetWindow<SampleGameManagerWindow>();
            window.titleContent = new GUIContent(MENU_ITEM_NAME);
            window.Show();
        }

        /// <summary>
        /// OnGUI
        /// </summary>
        private void OnGUI()
        {
            bool enabled = EditorApplication.isPlaying && SceneManager.GetActiveScene().name.Equals("InGame");

            // �v���C���̂ݗL���\��
            var color = GUI.color;
            if(enabled)
            {
                GUI.color = Color.green;
            }
            else
            {
                GUI.color = Color.red;
            }
            using (new EditorGUILayout.VerticalScope("Box", GUILayout.ExpandWidth(true)))
            {
                GUI.color = color;
                EditorGUILayout.LabelField("�v���C�����C���Q�[���ł̂ݗL��", EditorStyles.boldLabel);
            }

            using (new EditorGUI.DisabledGroupScope(!enabled))
            {
                // �Q�[���X�e�[�g
                EditorGUILayout.LabelField("�Q�[���X�e�[�g", EditorStyles.boldLabel);

                if (GUILayout.Button("�Q�[���J�n"))
                {
                    SampleGameManager.Instance.StartGame();
                }
                if (GUILayout.Button("�Q�[���I��"))
                {
                    SampleGameManager.Instance.EndGame();
                }

                GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));

                // �^�C�}�[
                EditorGUILayout.LabelField("�^�C�}�[", EditorStyles.boldLabel);

                if (GUILayout.Button("�^�C�}�[�ĊJ"))
                {
                    SampleGameManager.Instance.ResumeTimer();
                }
                if (GUILayout.Button("�^�C�}�[��~"))
                {
                    SampleGameManager.Instance.PauseTimer();
                }

                GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));

                // �X�R�A
                EditorGUILayout.LabelField("�X�R�A", EditorStyles.boldLabel);

                foreach (var value in SCORE_MODIFIER_VALUES)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button((-value).ToString()))
                    {
                        SampleGameManager.Instance.DeductScore(value);
                    }
                    if (GUILayout.Button(value.ToString()))
                    {
                        SampleGameManager.Instance.AddScore(value);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("���Z�b�g"))
                {
                    SampleGameManager.Instance.ResetScore();
                }

                GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
            }

            // �Z�[�u�f�[�^
            EditorGUILayout.LabelField("�Z�[�u�f�[�^", EditorStyles.boldLabel);

            if (GUILayout.Button("�폜"))
            {
                PlayerPrefs.DeleteAll();
            }

            using (new EditorGUI.DisabledScope(true))
            {
                float time = 0f;
                int score = 0;
                if (EditorApplication.isPlaying)
                {
                    time = PlayerPrefs.GetFloat(SampleGameManager.TIME_PLAYER_PREFS_KEY);
                    score = PlayerPrefs.GetInt(SampleGameManager.SCORE_PLAYER_PREFS_KEY);
                }

                EditorGUILayout.FloatField("����", time);
                EditorGUILayout.IntField("�X�R�A", score);
            }
        }
    }
}
