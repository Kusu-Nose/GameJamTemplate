using HackathonTemplate.UIs;
using HackathonTemplate.Utilities;
using UnityEngine;

/// <summary>
/// �T���v���̃Q�[���}�l�[�W���[
/// </summary>
public class SampleGameManager : SingletonMonoBehaviour<SampleGameManager>
{
    /// <summary>
    /// �Q�[���̏��
    /// </summary>
    enum GameState
    {
        NotStarted,
        Playing,
        Ended
    }

    /// <summary>
    /// ���Ԃ̕ۑ��p�L�[
    /// </summary>
    public static string TIME_PLAYER_PREFS_KEY = "Time";
    /// <summary>
    /// �X�R�A�̕ۑ��p�L�[
    /// </summary>
    public static string SCORE_PLAYER_PREFS_KEY = "Score";
    /// <summary>
    /// �Q�[���̏��
    /// </summary>
    private GameState _gameState = GameState.NotStarted;

    /// <summary>
    /// �Q�[���J�n
    /// </summary>
    public void StartGame()
    {
        if (_gameState != GameState.NotStarted)
        {
            return;
        }
        _gameState = GameState.Playing;

        PlayerPrefs.SetInt(SCORE_PLAYER_PREFS_KEY, 0);
        PlayerPrefs.SetFloat(TIME_PLAYER_PREFS_KEY, 0);

        Timer.Instance.StartTimer();
    }

    /// <summary>
    /// �Q�[���I��
    /// </summary>
    public void EndGame()
    {
        if (_gameState != GameState.Playing)
        {
            return;
        }
        _gameState = GameState.Ended;

        PlayerPrefs.SetInt(SCORE_PLAYER_PREFS_KEY, ScoreCounter.Instance.CurrentScore);
        PlayerPrefs.SetFloat(TIME_PLAYER_PREFS_KEY, Timer.Instance.CurrentTime);

        Timer.Instance.FinishTimer();
    }

    /// <summary>
    /// �^�C�}�[��~
    /// </summary>
    public void PauseTimer()
    {
        if (_gameState != GameState.Playing)
        {
            return;
        }
        Timer.Instance.PauseTimer();
    }

    /// <summary>
    /// �^�C�}�[�ĊJ
    /// </summary>
    public void ResumeTimer()
    {
        if (_gameState != GameState.Playing)
        {
            return;
        }
        Timer.Instance.ResumeTimer();
    }

    /// <summary>
    /// �X�R�A���Z
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {
        ScoreCounter.Instance.AddScore(score);
    }

    /// <summary>
    /// �X�R�A���Z
    /// </summary>
    /// <param name="score"></param>
    public void DeductScore(int score)
    {
        ScoreCounter.Instance.DeductScore(score);
    }

    /// <summary>
    /// �X�R�A���Z�b�g
    /// </summary>
    public void ResetScore()
    {
        ScoreCounter.Instance.SetScore(0);
    }
}
