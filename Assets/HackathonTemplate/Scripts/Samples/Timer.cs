using UnityEngine;
using UnityEngine.Events;
using HackathonTemplate.Utilities;
using static HackathonTemplate.UIs.Timer;

namespace HackathonTemplate.UIs
{
    /// <summary>
    /// ���Ԃ��v��
    /// </summary>
    public class Timer : SingletonMonoBehaviour<Timer>
    {
        /// <summary>
        /// �v�����
        /// </summary>
        public enum CountState
        {
            Initial,
            InProgress,
            Paused,
            Completed
        }
        /// <summary>
        /// �v�����@
        /// </summary>
        public enum CountDirection
        {
            Up,
            Down,
        }

        /// <summary>
        /// �L����
        /// </summary>
        [SerializeField, Label("�L����")]
        private bool _isEnabled = true;
        public bool IsActive => _isEnabled;
        // <summary>
        /// �v�����@
        /// </summary>
        [SerializeField, Label("�v�����@")]
        private CountDirection _countDirection = CountDirection.Down;
        /// <summary>
        /// �J�n����
        /// </summary>
        [SerializeField, Label("�J�n����")]
        private float _startTime = 60.0f;
        /// <summary>
        /// �I������
        /// </summary>
        [SerializeField, Label("�I������")]
        private float _endTime = 0.0f;

        /// <summary>
        /// �^�C�}�[�J�n���̃C�x���g
        /// </summary>
        [SerializeField, Label("�^�C�}�[�J�n���̃C�x���g")]
        private UnityEvent<float> _onTimerStarted = new();
        /// <summary>
        /// �^�C�}�[�I�����̃C�x���g
        /// </summary>
        [SerializeField, Label("�^�C�}�[�I�����̃C�x���g")]
        private UnityEvent<float> _onTimerFinished = new();
        /// <summary>
        /// �^�C�}�[��~���̃C�x���g
        /// </summary>
        [SerializeField, Label("�^�C�}�[��~���̃C�x���g")]
        private UnityEvent<float> _onTimerPaused = new();
        /// <summary>
        /// �^�C�}�[�ĊJ���̃C�x���g
        /// </summary>
        [SerializeField, Label("�^�C�}�[�ĊJ���̃C�x���g")]
        private UnityEvent<float> _onTimerResumed = new();

        /// <summary>
        /// ���ݎ���
        /// </summary>
        private float _currentTime;
        public float CurrentTime => _currentTime;

        /// <summary>
        /// �v�����
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
        /// �^�C�}�[�J�n
        /// </summary>
        public void StartTimer()
        {
            if (_countState != CountState.Initial)
            {
                Debug.LogWarning("�^�C�}�[�����ɊJ�n���Ă��邽�ߊJ�n�ł��܂���");
                return;
            }
            _countState = CountState.InProgress;

            SetTimerEnabled(true);
            _onTimerStarted.Invoke(_currentTime);
        }

        /// <summary>
        /// �^�C�}�[�I��
        /// </summary>
        public void FinishTimer()
        {
            if (_countState != CountState.InProgress)
            {
                Debug.LogWarning("�^�C�}�[�����ɏI�����Ă��邽�ߏI���ł��܂���");
                return;
            }
            _countState = CountState.InProgress;

            SetTimerEnabled(false);
            _onTimerFinished.Invoke(_currentTime);
        }

        /// <summary>
        /// �^�C�}�[��~
        /// </summary>
        public void PauseTimer()
        {
            if (_countState != CountState.InProgress)
            {
                Debug.LogWarning("�^�C�}�[���v�����ł͖������ߒ�~�ł��܂���");
                return;
            }
            _countState = CountState.Paused;

            SetTimerEnabled(false);
        }

        /// <summary>
        /// �^�C�}�[�ĊJ
        /// </summary>
        public void ResumeTimer()
        {
            if (_countState != CountState.Paused)
            {
                Debug.LogWarning("�^�C�}�[����~���ł͖������ߍĊJ�ł��܂���");
                return;
            }
            _countState = CountState.InProgress;

            SetTimerEnabled(true);
        }

        /// <summary>
        /// �^�C�}�[���L�������Z�b�g
        /// </summary>
        /// <param name="enabled"></param>
        public void SetTimerEnabled(bool enabled)
        {
            _isEnabled = enabled;
        }

        /// <summary>
        /// ���Ԃ�ݒ�
        /// </summary>
        /// <param name="value">����</param>
        public void SetTime(float time)
        {
            _currentTime = time;
        }

        /// <summary>
        /// �J�E���g����
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
        /// ���Ԃ̃J�E���g�A�b�v
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
        /// ���Ԃ̃J�E���g�_�E��
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