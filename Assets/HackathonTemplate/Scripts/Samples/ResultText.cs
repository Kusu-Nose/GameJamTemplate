using TMPro;
using UnityEngine;

/// <summary>
/// リザルトテキスト
/// </summary>

public class ResultText : MonoBehaviour
{
    /// <summary>
    /// テキスト
    /// </summary>
    TextMeshProUGUI _text;

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
        // PrayerPrefsからスコアとタイムを取得
        float score = PlayerPrefs.GetInt(SampleGameManager.SCORE_PLAYER_PREFS_KEY);
        float time = PlayerPrefs.GetFloat(SampleGameManager.TIME_PLAYER_PREFS_KEY);

        _text.text = "Time : " + time + "\n" + "Score : " + score;
    }
}
