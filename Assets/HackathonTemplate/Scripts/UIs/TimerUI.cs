using System;
using UnityEngine;
using TMPro;
using HackathonTemplate.Utilities;

namespace HackathonTemplate.UIs
{
    /// <summary>
    /// �^�C�}�[��UI
    /// </summary>
    public class TimerUI : MonoBehaviour
    {
        /// <summary>
        /// �ړ���
        /// </summary>
        [SerializeField, Label("�ړ���")]
        private string _prefix = "";
        /// <summary>
        /// �ڔ���
        /// </summary>
        [SerializeField, Label("�ڔ���")]
        private string _suffix = "";

        [Header("��")]
        /// <summary>
        /// ����\�����邩
        /// </summary>
        [SerializeField, Label("����\�����邩")]
        private bool _isShowMinute = false;
        /// <summary>
        /// ���̌���
        /// </summary>
        [SerializeField, Label("���̌���"), EnableIf(nameof(_isShowMinute))]
        private int _minuteDigits = 2;
        /// <summary>
        /// ���ƕb�̋�؂蕶��
        /// </summary>
        [SerializeField, Label("���ƕb�̋�؂蕶��"), EnableIf(nameof(_isShowMinute))]
        private string _minuteSecondSeparator = ":";

        [Header("�b")]
        /// <summary>
        /// �b��\�����邩
        /// </summary>
        [SerializeField, Label("�b��\�����邩")]
        private bool _isShowSecond = true;
        /// <summary>
        /// �b�̌���
        /// </summary>
        [SerializeField, Label("�b�̌���"), EnableIf(nameof(_isShowSecond))]
        private int _secondDigits = 2;

        [Header("����")]
        /// <summary>
        /// ������\�����邩
        /// </summary>
        [SerializeField, Label("������\�����邩")]
        private bool _isShowDecimal = false;
        /// <summary>
        /// �����̌���
        /// </summary>
        [SerializeField, Label("�����̌���"), EnableIf(nameof(_isShowDecimal))]
        private int _decimalDigits = 2;
        /// <summary>
        /// �b�Ə����̋�؂蕶��
        /// </summary>
        [SerializeField, Label("�b�Ə����̋�؂蕶��"), EnableIf(nameof(_isShowDecimal))]
        private string _secondDecimalSeparate = ".";

        /// <summary>
        /// �\���p�e�L�X�g
        /// </summary>
        private TextMeshProUGUI _text;
        /// <summary>
        /// �^�C�}�[
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
                // ���Ԃ̕�����
                PrintTime(_timer.CurrentTime);
            }
            catch (NullReferenceException)
            {
                Debug.LogWarning("Timer ���A�^�b�`����Ă��܂���");
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
        /// ���Ԃ̕�����
        /// </summary>
        private void PrintTime(float time)
        {
            // �\������e�L�X�g�ւ̔��f
            _text.text = "";

            // �ړ���
            if (_prefix != "")
            {
                _text.text += _prefix;
            }

            int minutes = 0;
            int seconds = 0;
            if (_isShowMinute)
            {
                //��
                minutes = (int)Mathf.Abs(time / 60);
                //�b
                seconds = (int)Mathf.Abs(time % 60);
            }
            else
            {
                //�b
                seconds = (int)Mathf.Abs(time);
            }

            //����
            float decimalSeconds = 0.0f;
            if (time >= 0.0f)
            {
                decimalSeconds = time - MathF.Floor(time);
            }
            else
            {
                decimalSeconds = MathF.Ceiling(time) - time;
                //0�ȉ��̏ꍇ�̓}�C�i�X��\��
                _text.text += "-";
            }

            //���𕶎���
            if (_isShowMinute)
            {
                //0����
                _text.text += minutes.ToString().PadLeft(_minuteDigits, '0');
                _text.text += _minuteSecondSeparator;
            }
            //�b�𕶎���
            if (_isShowSecond)
            {
                //0����
                _text.text += seconds.ToString().PadLeft(_secondDigits, '0');
            }
            //�����𕶎���
            if (_isShowDecimal)
            {
                _text.text += _secondDecimalSeparate;

                //�����_�ȉ��𕶎���
                string decimalSecondsStr = decimalSeconds.ToString("." + new string('#', _decimalDigits));
                //�����_���擪�ɂ���ꍇ�͍폜
                if (decimalSecondsStr.StartsWith('.'))
                {
                    decimalSecondsStr = decimalSecondsStr.Substring(1);
                }
                //0����
                decimalSecondsStr = decimalSecondsStr.PadRight(_decimalDigits, '0');
                _text.text += decimalSecondsStr;
            }

            // �ڔ���
            if (_suffix != "")
            {
                _text.text += _suffix;
            }
        }
    }
}