using System;
using UnityEngine;
using TMPro;
using HackathonTemplate.Utilities;

namespace HackathonTemplate.UIs
{
    /// <summary>
    /// タイマーのUI
    /// </summary>
    public class TimerUI : MonoBehaviour
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

        [Header("分")]
        /// <summary>
        /// 分を表示するか
        /// </summary>
        [SerializeField, Label("分を表示するか")]
        private bool _isShowMinute = false;
        /// <summary>
        /// 分の桁数
        /// </summary>
        [SerializeField, Label("分の桁数"), EnableIf(nameof(_isShowMinute))]
        private int _minuteDigits = 2;
        /// <summary>
        /// 分と秒の区切り文字
        /// </summary>
        [SerializeField, Label("分と秒の区切り文字"), EnableIf(nameof(_isShowMinute))]
        private string _minuteSecondSeparator = ":";

        [Header("秒")]
        /// <summary>
        /// 秒を表示するか
        /// </summary>
        [SerializeField, Label("秒を表示するか")]
        private bool _isShowSecond = true;
        /// <summary>
        /// 秒の桁数
        /// </summary>
        [SerializeField, Label("秒の桁数"), EnableIf(nameof(_isShowSecond))]
        private int _secondDigits = 2;

        [Header("小数")]
        /// <summary>
        /// 小数を表示するか
        /// </summary>
        [SerializeField, Label("小数を表示するか")]
        private bool _isShowDecimal = false;
        /// <summary>
        /// 小数の桁数
        /// </summary>
        [SerializeField, Label("小数の桁数"), EnableIf(nameof(_isShowDecimal))]
        private int _decimalDigits = 2;
        /// <summary>
        /// 秒と小数の区切り文字
        /// </summary>
        [SerializeField, Label("秒と小数の区切り文字"), EnableIf(nameof(_isShowDecimal))]
        private string _secondDecimalSeparate = ".";

        /// <summary>
        /// 表示用テキスト
        /// </summary>
        private TextMeshProUGUI _text;
        /// <summary>
        /// タイマー
        /// </summary>
        private Timer _timer;

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
            _timer = Timer.Instance;
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            try
            {
                // 時間の文字列化
                PrintTime(_timer.CurrentTime);
            }
            catch (NullReferenceException)
            {
                Debug.LogWarning("Timer がアタッチされていません");
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
            PrintTime(0.0f);
        }

        /// <summary>
        /// 時間の文字列化
        /// </summary>
        private void PrintTime(float time)
        {
            // 表示するテキストへの反映
            _text.text = "";

            // 接頭辞
            if (_prefix != "")
            {
                _text.text += _prefix;
            }

            int minutes = 0;
            int seconds = 0;
            if (_isShowMinute)
            {
                //分
                minutes = (int)Mathf.Abs(time / 60);
                //秒
                seconds = (int)Mathf.Abs(time % 60);
            }
            else
            {
                //秒
                seconds = (int)Mathf.Abs(time);
            }

            //小数
            float decimalSeconds = 0.0f;
            if (time >= 0.0f)
            {
                decimalSeconds = time - MathF.Floor(time);
            }
            else
            {
                decimalSeconds = MathF.Ceiling(time) - time;
                //0以下の場合はマイナスを表示
                _text.text += "-";
            }

            //分を文字列化
            if (_isShowMinute)
            {
                //0埋め
                _text.text += minutes.ToString().PadLeft(_minuteDigits, '0');
                _text.text += _minuteSecondSeparator;
            }
            //秒を文字列化
            if (_isShowSecond)
            {
                //0埋め
                _text.text += seconds.ToString().PadLeft(_secondDigits, '0');
            }
            //小数を文字列化
            if (_isShowDecimal)
            {
                _text.text += _secondDecimalSeparate;

                //小数点以下を文字列化
                string decimalSecondsStr = decimalSeconds.ToString("." + new string('#', _decimalDigits));
                //小数点が先頭にある場合は削除
                if (decimalSecondsStr.StartsWith('.'))
                {
                    decimalSecondsStr = decimalSecondsStr.Substring(1);
                }
                //0埋め
                decimalSecondsStr = decimalSecondsStr.PadRight(_decimalDigits, '0');
                _text.text += decimalSecondsStr;
            }

            // 接尾辞
            if (_suffix != "")
            {
                _text.text += _suffix;
            }
        }
    }
}