using UnityEngine;
using UnityEngine.Events;
using HackathonTemplate.Utilities;

namespace HackathonTemplate.UIs
{
    /// <summary>
    /// スコアカウンター
    /// </summary>
    public class ScoreCounter : SingletonMonoBehaviour<ScoreCounter>
    {
        /// <summary>
        /// 初期スコア
        /// </summary>
        [SerializeField, Label("初期スコア")]
        private int _defaultScore = 0;

        /// <summary>
        /// 目標スコアが有効か
        /// </summary>
        [SerializeField, Label("目標スコアが有効か")]
        private bool _isTargetScoreEnabled = false;

        /// <summary>
        /// 目標スコア
        /// </summary>
        [EnableIf(nameof(_isTargetScoreEnabled))]
        [SerializeField, Label("目標スコア")]
        private int _targetScore = 10;

        /// <summary>
        /// スコア加算時のイベント
        /// </summary>
        [SerializeField, Label("スコア加算時のイベント")]
        private UnityEvent<int> _onScoreAdded = new();
        /// <summary>
        /// スコア減算時のイベント
        /// </summary>
        [SerializeField, Label("スコア減算時のイベント")]
        private UnityEvent<int> _onScoreDeducted = new();
        /// <summary>
        /// スコアセット時のイベント
        /// </summary>
        [SerializeField, Label("スコアセット時のイベント")]
        private UnityEvent<int> _onScoreSetted = new();
        /// <summary>
        /// 目標スコア到達時のイベント
        /// </summary>
        [SerializeField, Label("目標スコア到達時のイベント")]
        private UnityEvent<int> _onScoreTargetReached = new();

        /// <summary>
        /// 現在のスコア
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
        /// スコア加算
        /// </summary>
        /// <param name="score">スコア</param>
        public void AddScore(int score)
        {
            _currentScore += score;

            _onScoreAdded.Invoke(_currentScore);
            CheckScore();
        }

        /// <summary>
        /// スコア減算
        /// </summary>
        /// <param name="score">スコア</param>
        public void DeductScore(int score)
        {
            _currentScore -= score;

            _onScoreDeducted.Invoke(_currentScore);
            CheckScore();
        }

        /// <summary>
        /// スコアをセット
        /// </summary>
        /// <param name="score"></param>
        public void SetScore(int score)
        {
            _currentScore = score;

            _onScoreSetted.Invoke(_currentScore);
            CheckScore();
        }

        /// <summary>
        /// スコア確認
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