using System.IO;
using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    public static BestScore Instance { get; private set; }
    public HighScore highScore;
    public string playerName = "Anonymous";

    public TextMeshProUGUI bestScore;

    private void Awake()
    {
        // Ensure that only one instance of MainManager exists
        Debug.Log("BestScore Awake called");
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
        highScore ??= new HighScore(playerName, 0);
    }

    [System.Serializable]
    class SaveData
    {
        public string Name;
        public int Score;
    }

    private void DisplayBestScore(HighScore highScore)
    {
        bestScore.text = $"Best Score : {highScore.Name} : {highScore.Score}";
    }


    public void SaveHighScore()
    {
        Debug.Log($"Saving High Score: {highScore}");
        SaveData data = new SaveData();
        data.Name = highScore.Name;
        data.Score = highScore.Score;

        string jsonData = JsonUtility.ToJson(data);
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
            SaveData data = JsonUtility.FromJson<SaveData>(jsonData);
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
