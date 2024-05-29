using TMPro;
using UnityEngine;

/// <summary>
/// ���U���g�e�L�X�g
/// </summary>

public class ResultText : MonoBehaviour
{
    /// <summary>
    /// �e�L�X�g
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
        // PrayerPrefs����X�R�A�ƃ^�C�����擾
        float score = PlayerPrefs.GetInt(SampleGameManager.SCORE_PLAYER_PREFS_KEY);
        float time = PlayerPrefs.GetFloat(SampleGameManager.TIME_PLAYER_PREFS_KEY);

        _text.text = "Time : " + time + "\n" + "Score : " + score;
    }
}
