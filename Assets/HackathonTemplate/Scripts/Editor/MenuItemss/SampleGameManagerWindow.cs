using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using HackathonTemplate.UIs;

namespace HackathonTemplate.Editor
{
    public partial class SampleGameManagerWindow : EditorWindow
    {
        /// <summary>
        /// メニューアイテムの名前
        /// </summary>
        private const string MENU_ITEM_NAME = "SampleGameManager";
        /// <summary>
        /// メニューアイテムの優先度
        /// </summary>
        private const int MENU_ITEM_PRIORITY = 0;
        /// <summary>
        /// スコア増減値
        /// </summary>
        private static readonly int[] SCORE_MODIFIER_VALUES = new[]{ 1, 10, 100, 1000 };

        /// <summary>
        /// ウィンドウ表示
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

            // プレイ中のみ有効表示
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
                EditorGUILayout.LabelField("プレイ中かつインゲームでのみ有効", EditorStyles.boldLabel);
            }

            using (new EditorGUI.DisabledGroupScope(!enabled))
            {
                // ゲームステート
                EditorGUILayout.LabelField("ゲームステート", EditorStyles.boldLabel);

                if (GUILayout.Button("ゲーム開始"))
                {
                    SampleGameManager.Instance.StartGame();
                }
                if (GUILayout.Button("ゲーム終了"))
                {
                    SampleGameManager.Instance.EndGame();
                }

                GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));

                // タイマー
                EditorGUILayout.LabelField("タイマー", EditorStyles.boldLabel);

                if (GUILayout.Button("タイマー再開"))
                {
                    SampleGameManager.Instance.ResumeTimer();
                }
                if (GUILayout.Button("タイマー停止"))
                {
                    SampleGameManager.Instance.PauseTimer();
                }

                GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));

                // スコア
                EditorGUILayout.LabelField("スコア", EditorStyles.boldLabel);

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

                if (GUILayout.Button("リセット"))
                {
                    SampleGameManager.Instance.ResetScore();
                }

                GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
            }

            // セーブデータ
            EditorGUILayout.LabelField("セーブデータ", EditorStyles.boldLabel);

            if (GUILayout.Button("削除"))
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

                EditorGUILayout.FloatField("時間", time);
                EditorGUILayout.IntField("スコア", score);
            }
        }
    }
}
