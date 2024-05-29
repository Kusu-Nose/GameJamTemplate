using System;
using UnityEngine;
using TMPro;
using HackathonTemplate.Utilities;

namespace HackathonTemplate.UIs
{
    /// <summary>
    /// �X�R�A�J�E���^�[��UI
    /// </summary>
    public class ScoreCounterUI : MonoBehaviour
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

        [Header("�X�R�A")]
        /// <summary>
        /// ����
        /// </summary>
        [SerializeField, Label("����")]
        private int _digits = 4;

        /// <summary>
        /// �\���p�e�L�X�g
        /// </summary>
        private TextMeshProUGUI _text;
        /// <summary>
        /// �X�R�A�J�E���^�[
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
                // ���Ԃ̕�����
                PrintScore(_scoreCounter.CurrentScore);
            }
            catch (NullReferenceException)
            {
                Debug.LogWarning("ScoreCounter ���A�^�b�`����Ă��܂���");
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
        /// ���Ԃ̕�����\��
        /// </summary>
        private void PrintScore(int score)
        {
            _text.text = "";

            // �ړ���
            if(_prefix != "")
            {
                _text.text += _prefix;
            }

            // �X�R�A
            if(score < 0)
            {
                _text.text += "-";
            }
            _text.text += Mathf.Abs(score).ToString().PadLeft(_digits, '0');

            // �ڔ���
            if(_suffix != "")
            {
                _text.text += _suffix;
            }
        }
    }
}

