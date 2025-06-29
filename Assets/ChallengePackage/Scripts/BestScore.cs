using System.IO;
using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    public static BestScore Instance { get; private set; }
    public HighScore highScore;
    private string _currentPlayerName;
    public string CurrentPlayerName
    {
        get => string.IsNullOrWhiteSpace(_currentPlayerName) ? "Anonymous" : _currentPlayerName;
        set
        {
            _currentPlayerName = string.IsNullOrWhiteSpace(value) ? "Anonymous" : value;
            Debug.Log($"Current Player Name set to: {_currentPlayerName}");
        }
    }

    public TMP_Text bestScore;

    public string GetBestScore(HighScore highScore)
    {
        return $"Best Score : {highScore.Name} : {highScore.Score}";
    }

    private void DisplayBestScore(HighScore highScore)
    {
        if (bestScore != null)
        {
            bestScore.text = GetBestScore(highScore);
        }
        else
        {
            Debug.LogWarning("Best Score Text is not assigned.");
        }
    }

    private void Awake()
    {
        // Ensure that only one instance of MainManager exists
        Debug.Log("BestScore Awake called");
        if (Instance != null)
        {
            Debug.Log("BestScore Awake methood, about to Destroy(gameObject).");
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
        if (highScore == null)
        {
            Debug.LogWarning("HighScore is null, initializing with default values.");
            highScore = new HighScore(CurrentPlayerName, 0);
        }
    }

    public void SaveHighScore(string playerName, int score)
    {
        Debug.Log($"Saving High Score: {highScore}");
        highScore = new HighScore(playerName, score);

        string jsonData = JsonUtility.ToJson(highScore);
        string pathName = Path.Combine(Application.persistentDataPath, "breakout.json");
        Debug.Log($"Saving to path: {pathName.Replace("/", "\\")}");
        File.WriteAllText(pathName, jsonData);
        DisplayBestScore(highScore);
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
        }
        else
        {
            Debug.LogWarning("Save file not found, using default high score.");
            highScore = new HighScore("Anonymous", 0);
        }

        Debug.Log($"HighScore Name: {highScore.Name}, HighScore Score: {highScore.Score}");
        DisplayBestScore(highScore);
    }
}
