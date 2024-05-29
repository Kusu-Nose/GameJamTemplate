using UnityEngine;
using UnityEngine.Events;
using HackathonTemplate.Utilities;

namespace HackathonTemplate.UIs
{
    /// <summary>
    /// �X�R�A�J�E���^�[
    /// </summary>
    public class ScoreCounter : SingletonMonoBehaviour<ScoreCounter>
    {
        /// <summary>
        /// �����X�R�A
        /// </summary>
        [SerializeField, Label("�����X�R�A")]
        private int _defaultScore = 0;

        /// <summary>
        /// �ڕW�X�R�A���L����
        /// </summary>
        [SerializeField, Label("�ڕW�X�R�A���L����")]
        private bool _isTargetScoreEnabled = false;

        /// <summary>
        /// �ڕW�X�R�A
        /// </summary>
        [EnableIf(nameof(_isTargetScoreEnabled))]
        [SerializeField, Label("�ڕW�X�R�A")]
        private int _targetScore = 10;

        /// <summary>
        /// �X�R�A���Z���̃C�x���g
        /// </summary>
        [SerializeField, Label("�X�R�A���Z���̃C�x���g")]
        private UnityEvent<int> _onScoreAdded = new();
        /// <summary>
        /// �X�R�A���Z���̃C�x���g
        /// </summary>
        [SerializeField, Label("�X�R�A���Z���̃C�x���g")]
        private UnityEvent<int> _onScoreDeducted = new();
        /// <summary>
        /// �X�R�A�Z�b�g���̃C�x���g
        /// </summary>
        [SerializeField, Label("�X�R�A�Z�b�g���̃C�x���g")]
        private UnityEvent<int> _onScoreSetted = new();
        /// <summary>
        /// �ڕW�X�R�A���B���̃C�x���g
        /// </summary>
        [SerializeField, Label("�ڕW�X�R�A���B���̃C�x���g")]
        private UnityEvent<int> _onScoreTargetReached = new();

        /// <summary>
        /// ���݂̃X�R�A
        /// </summary>
        private int _currentScore = 0;
        public int CurrentScore => _currentScore;

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            SetScore(_defaultScore);
        }

        /// <summary>
        /// �X�R�A���Z
        /// </summary>
        /// <param name="score">�X�R�A</param>
        public void AddScore(int score)
        {
            _currentScore += score;

            _onScoreAdded.Invoke(_currentScore);
            CheckScore();
        }

        /// <summary>
        /// �X�R�A���Z
        /// </summary>
        /// <param name="score">�X�R�A</param>
        public void DeductScore(int score)
        {
            _currentScore -= score;

            _onScoreDeducted.Invoke(_currentScore);
            CheckScore();
        }

        /// <summary>
        /// �X�R�A���Z�b�g
        /// </summary>
        /// <param name="score"></param>
        public void SetScore(int score)
        {
            _currentScore = score;

            _onScoreSetted.Invoke(_currentScore);
            CheckScore();
        }

        /// <summary>
        /// �X�R�A�m�F
        /// </summary>
        private void CheckScore()
        {
            if (false == _isTargetScoreEnabled)
                return;

            if (_currentScore >= _targetScore)
            {
                _onScoreTargetReached.Invoke(_currentScore);
            }
        }
    }
}