using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    public static BestScore Instance { get; private set; }
    public List<HighScore> highScores = new List<HighScore>();
    public string CurrentPlayerName = "Anonymous";
    public TMP_Text bestScore;

    private const int MaxHighScores = 10;

    [Serializable]
    class SaveData
    {
        public List<HighScore> HighScores = new List<HighScore>();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScores();
        if (highScores.Count == 0)
        {
            highScores.Add(new HighScore(CurrentPlayerName, 0, DateTime.Now));
        }
    }

    public void SaveHighScore(string playerName, int score)
    {
        // Add new score
        highScores.Add(new HighScore(playerName, score, DateTime.Now));
        // Sort descending and keep top 10
        highScores.Sort((a, b) => b.Score.CompareTo(a.Score));
        if (highScores.Count > MaxHighScores)
            highScores.RemoveRange(MaxHighScores, highScores.Count - MaxHighScores);

        // Save to file
        SaveData data = new SaveData { HighScores = highScores };
        string jsonData = JsonUtility.ToJson(data);
        string pathName = Path.Combine(Application.persistentDataPath, "breakout.json");
        File.WriteAllText(pathName, jsonData);

        DisplayBestScore();
    }

    public void LoadHighScores()
    {
        string pathName = Path.Combine(Application.persistentDataPath, "breakout.json");
        if (File.Exists(pathName))
        {
            string jsonData = File.ReadAllText(pathName);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonData);
            highScores = data.HighScores ?? new List<HighScore>();
        }
        else
        {
            highScores = new List<HighScore>();
        }
        DisplayBestScore();
    }

    public void DisplayBestScore()
    {
        if (bestScore != null && highScores.Count > 0)
        {
            var top = highScores[0];
            bestScore.text = $"Best Score : {top.Name} : {top.Score} ({top.DateTime})";
        }
    }

    // Utility for menu to get formatted high scores
    public string GetHighScoresText()
    {
        if (highScores.Count == 0) return "No high scores yet!";
        string result = "Top 10 High Scores:\n";
        for (int i = 0; i < highScores.Count; i++)
        {
            var hs = highScores[i];
            result += $"{i + 1}. {hs.Name} - {hs.Score} ({hs.DateTime})\n";
        }
        return result;
    }

    internal string GetBestScoreText(List<HighScore> highScores)
    {
        if (bestScore != null && highScores.Count > 0)
        {
            return $"Best Score : {highScores[0].Name} : {highScores[0].Score}";
        }

        return "Best Score : Anonymous : 0";
    }
}
