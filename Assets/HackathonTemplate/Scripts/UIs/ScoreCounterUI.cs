using System;
using UnityEngine;
using TMPro;
using HackathonTemplate.Utilities;

namespace HackathonTemplate.UIs
{
    /// <summary>
    /// スコアカウンターのUI
    /// </summary>
    public class ScoreCounterUI : MonoBehaviour
    {
        /// <summary>
        /// 接頭辞
        /// </summary>
        [SerializeField, Label("接頭辞")]
        private string _prefix = "";
        /// <summary>
        /// 接尾辞
        /// </summary>
        [SerializeField, Label("接尾辞")]
        private string _suffix = "";

        [Header("スコア")]
        /// <summary>
        /// 桁数
        /// </summary>
        [SerializeField, Label("桁数")]
        private int _digits = 4;

        /// <summary>
        /// 表示用テキスト
        /// </summary>
        private TextMeshProUGUI _text;
        /// <summary>
        /// スコアカウンター
        /// </summary>
        private ScoreCounter _scoreCounter;

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            _scoreCounter = ScoreCounter.Instance;
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            try
            {
                // 時間の文字列化
                PrintScore(_scoreCounter.CurrentScore);
            }
            catch (NullReferenceException)
            {
                Debug.LogWarning("ScoreCounter がアタッチされていません");
            }
        }

        /// <summary>
        /// OnValidate
        /// </summary>
        private void OnValidate()
        {
            if (_text == null)
            {
                _text = GetComponent<TextMeshProUGUI>();
            }
            PrintScore(0);
        }

        /// <summary>
        /// 時間の文字列表示
        /// </summary>
        private void PrintScore(int score)
        {
            _text.text = "";

            // 接頭辞
            if(_prefix != "")
            {
                _text.text += _prefix;
            }

            // スコア
            if(score < 0)
            {
                _text.text += "-";
            }
            _text.text += Mathf.Abs(score).ToString().PadLeft(_digits, '0');

            // 接尾辞
            if(_suffix != "")
            {
                _text.text += _suffix;
            }
        }
    }
}

