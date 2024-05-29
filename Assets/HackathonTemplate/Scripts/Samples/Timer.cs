using UnityEngine;
using UnityEngine.Events;
using HackathonTemplate.Utilities;
using static HackathonTemplate.UIs.Timer;

namespace HackathonTemplate.UIs
{
    /// <summary>
    /// 時間を計測
    /// </summary>
    public class Timer : SingletonMonoBehaviour<Timer>
    {
        /// <summary>
        /// 計測状態
        /// </summary>
        public enum CountState
        {
            Initial,
            InProgress,
            Paused,
            Completed
        }
        /// <summary>
        /// 計測方法
        /// </summary>
        public enum CountDirection
        {
            Up,
            Down,
        }

        /// <summary>
        /// 有効か
        /// </summary>
        [SerializeField, Label("有効か")]
        private bool _isEnabled = true;
        public bool IsActive => _isEnabled;
        // <summary>
        /// 計測方法
        /// </summary>
        [SerializeField, Label("計測方法")]
        private CountDirection _countDirection = CountDirection.Down;
        /// <summary>
        /// 開始時間
        /// </summary>
        [SerializeField, Label("開始時間")]
        private float _startTime = 60.0f;
        /// <summary>
        /// 終了時間
        /// </summary>
        [SerializeField, Label("終了時間")]
        private float _endTime = 0.0f;

        /// <summary>
        /// タイマー開始時のイベント
        /// </summary>
        [SerializeField, Label("タイマー開始時のイベント")]
        private UnityEvent<float> _onTimerStarted = new();
        /// <summary>
        /// タイマー終了時のイベント
        /// </summary>
        [SerializeField, Label("タイマー終了時のイベント")]
        private UnityEvent<float> _onTimerFinished = new();
        /// <summary>
        /// タイマー停止時のイベント
        /// </summary>
        [SerializeField, Label("タイマー停止時のイベント")]
        private UnityEvent<float> _onTimerPaused = new();
        /// <summary>
        /// タイマー再開時のイベント
        /// </summary>
        [SerializeField, Label("タイマー再開時のイベント")]
        private UnityEvent<float> _onTimerResumed = new();

        /// <summary>
        /// 現在時間
        /// </summary>
        private float _currentTime;
        public float CurrentTime => _currentTime;

        /// <summary>
        /// 計測状態
        /// </summary>
        private CountState _countState = CountState.Initial;

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            SetTime(_startTime);
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (_isEnabled)
            {
                Count();
            }
        }

        /// <summary>
        /// タイマー開始
        /// </summary>
        public void StartTimer()
        {
            if (_countState != CountState.Initial)
            {
                Debug.LogWarning("タイマーが既に開始しているため開始できません");
                return;
            }
            _countState = CountState.InProgress;

            SetTimerEnabled(true);
            _onTimerStarted.Invoke(_currentTime);
        }

        /// <summary>
        /// タイマー終了
        /// </summary>
        public void FinishTimer()
        {
            if (_countState != CountState.InProgress)
            {
                Debug.LogWarning("タイマーが既に終了しているため終了できません");
                return;
            }
            _countState = CountState.InProgress;

            SetTimerEnabled(false);
            _onTimerFinished.Invoke(_currentTime);
        }

        /// <summary>
        /// タイマー停止
        /// </summary>
        public void PauseTimer()
        {
            if (_countState != CountState.InProgress)
            {
                Debug.LogWarning("タイマーが計測中では無いため停止できません");
                return;
            }
            _countState = CountState.Paused;

            SetTimerEnabled(false);
        }

        /// <summary>
        /// タイマー再開
        /// </summary>
        public void ResumeTimer()
        {
            if (_countState != CountState.Paused)
            {
                Debug.LogWarning("タイマーが停止中では無いため再開できません");
                return;
            }
            _countState = CountState.InProgress;

            SetTimerEnabled(true);
        }

        /// <summary>
        /// タイマーが有効かをセット
        /// </summary>
        /// <param name="enabled"></param>
        public void SetTimerEnabled(bool enabled)
        {
            _isEnabled = enabled;
        }

        /// <summary>
        /// 時間を設定
        /// </summary>
        /// <param name="value">時間</param>
        public void SetTime(float time)
        {
            _currentTime = time;
        }

        /// <summary>
        /// カウント処理
        /// </summary>
        private void Count()
        {
            switch (_countDirection)
            {
                case CountDirection.Up: 
                    CountUp();
                    break;
                case CountDirection.Down:
                    CountDown();
                    break;
            }
        }

        /// <summary>
        /// 時間のカウントアップ
        /// </summary>
        private void CountUp()
        {
            _currentTime += Time.deltaTime;

            if(_currentTime >= _endTime)
            {
                FinishTimer();
            }
        }

        /// <summary>
        /// 時間のカウントダウン
        /// </summary>
        private void CountDown()
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= _endTime)
            {
                FinishTimer();
            }
        }
    }
}