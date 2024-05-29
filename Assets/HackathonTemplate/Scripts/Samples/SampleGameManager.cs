using HackathonTemplate.UIs;
using HackathonTemplate.Utilities;
using UnityEngine;

/// <summary>
/// サンプルのゲームマネージャー
/// </summary>
public class SampleGameManager : SingletonMonoBehaviour<SampleGameManager>
{
    /// <summary>
    /// ゲームの状態
    /// </summary>
    enum GameState
    {
        NotStarted,
        Playing,
        Ended
    }

    /// <summary>
    /// 時間の保存用キー
    /// </summary>
    public static string TIME_PLAYER_PREFS_KEY = "Time";
    /// <summary>
    /// スコアの保存用キー
    /// </summary>
    public static string SCORE_PLAYER_PREFS_KEY = "Score";
    /// <summary>
    /// ゲームの状態
    /// </summary>
    private GameState _gameState = GameState.NotStarted;

    /// <summary>
    /// ゲーム開始
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
    /// ゲーム終了
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
    /// タイマー停止
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
    /// タイマー再開
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
    /// スコア加算
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int score)
    {
        ScoreCounter.Instance.AddScore(score);
    }

    /// <summary>
    /// スコア減算
    /// </summary>
    /// <param name="score"></param>
    public void DeductScore(int score)
    {
        ScoreCounter.Instance.DeductScore(score);
    }

    /// <summary>
    /// スコアリセット
    /// </summary>
    public void ResetScore()
    {
        ScoreCounter.Instance.SetScore(0);
    }
}
