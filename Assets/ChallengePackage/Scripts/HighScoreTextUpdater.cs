using TMPro;
using UnityEngine;

public class HighScoreTextUpdater : MonoBehaviour
{
    public TMP_Text highScoreText;

    private void OnEnable()
    {
        BestScoreSingleton.OnHighScoreUpdated += UpdateBestScore;
        UpdateBestScore();
    }

    private void OnDisable()
    {
        BestScoreSingleton.OnHighScoreUpdated -= UpdateBestScore;
    }

    void UpdateBestScore()
    {
        if (BestScoreSingleton.Instance == null)
        {
            Debug.LogWarning("BestScore.Instance is null, cannot update high score text.");
            return;
        }

        if (highScoreText == null)
        {
            Debug.LogWarning("highScoreText is not assigned!");
            return;
        }

        HighScore hs = BestScoreSingleton.Instance.highScore;
        highScoreText.text = $"Best Score : {hs.Name} : {hs.Score}";
    }
}
 