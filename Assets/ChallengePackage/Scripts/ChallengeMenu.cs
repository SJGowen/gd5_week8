using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1000)]

public class ChallengeMenu : MonoBehaviour
{
    public TMP_InputField playerName;

    private void Start()
    {
        // Ensure that the BestScore instance is created and loaded
        if (BestScore.Instance == null)
        {
            Debug.LogError("BestScore instance is not initialized.");
            return;
        }
    }

    public void StartGame()
    {
        string currentPlayer = playerName.text;
        if (string.IsNullOrWhiteSpace(currentPlayer))
        {
            currentPlayer = "Anonymous";
        }

        // Store the player name in case they beet the high score
        BestScore.Instance.CurrentPlayerName = currentPlayer;

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
