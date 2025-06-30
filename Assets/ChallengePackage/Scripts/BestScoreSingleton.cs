using System;
using System.IO;
using UnityEngine;

public class BestScoreSingleton : MonoBehaviour
{
    public static BestScoreSingleton Instance { get; private set; }
    public static event Action OnHighScoreUpdated;
    public HighScore highScore;

    public string CurrentPlayerName;

    private void Awake()
    {
        // Ensure that only one instance of MainManager exists
        Debug.Log("BestScore Awake called");
        if (Instance != null)
        {
            Debug.Log("BestScore Awake methood, about to Destroy(gameObject).");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
        if (highScore == null)
        {
            Debug.LogWarning("HighScore is null, initializing with default values.");
        }
    }

    public void SetPlayerName(string playerName)
    {
        Debug.Log($"Setting CurrentPlayerName to: {playerName}");
        CurrentPlayerName = playerName;
    }

    public void SaveHighScore(string playerName, int score)
    {
        Debug.Log($"Saving High Score: {highScore}");
        highScore = new HighScore(playerName, score);

        string jsonData = JsonUtility.ToJson(highScore);
        string pathName = Path.Combine(Application.persistentDataPath, "breakout.json");
        Debug.Log($"Saving to path: {pathName.Replace("/", "\\")}");
        File.WriteAllText(pathName, jsonData);
    }

    public void LoadHighScore()
    {
        string pathName = Path.Combine(Application.persistentDataPath, "breakout.json");
        Debug.Log($"Loading from path: {pathName.Replace("/", "\\")}");
        if (File.Exists(pathName))
        {
            string jsonData = File.ReadAllText(pathName);
            HighScore data = JsonUtility.FromJson<HighScore>(jsonData);
            highScore = new HighScore(data.Name, data.Score);
            OnHighScoreUpdated?.Invoke();
        }
        else
        {
            Debug.LogWarning("Save file not found, using default high score.");
            highScore = new HighScore("Anonymous", 0);
        }

        Debug.Log($"HighScore Name: {highScore.Name}, HighScore Score: {highScore.Score}");
    }

    public void RecordScore(int score)
    {
        Debug.Log($"New Score for: {CurrentPlayerName} with score: {score}");
        if (highScore == null || score > highScore.Score)
        {
            Debug.Log($"New High Score for: {CurrentPlayerName} with score: {score}");
            SaveHighScore(CurrentPlayerName, score);
            OnHighScoreUpdated?.Invoke();
        }
    }
}
