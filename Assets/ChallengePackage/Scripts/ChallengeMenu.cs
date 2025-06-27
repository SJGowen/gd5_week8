using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1000)]

public class ChallengeMenu : MonoBehaviour
{
    public TextMeshProUGUI playerName;

    private void Start()
    {
        // Ensure that the BestScore instance is created and loaded
        if (BestScore.Instance == null)
        {
            Debug.LogError("BestScore instance is not initialized.");
            return;
        }
        BestScore.Instance.LoadHighScore();
    }

    public void StartGame()
    {
        string currentPlayer = playerName.text.Trim();
        if (string.IsNullOrEmpty(currentPlayer))
        {
            currentPlayer = "Anonymous";
        }
        
        BestScore.Instance.playerName = currentPlayer;

        SceneManager.LoadScene("challenge_main");
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        // If running in the editor, stop playing
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
