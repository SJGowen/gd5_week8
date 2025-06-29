using System;

[System.Serializable]
public class HighScore
{
    public string Name;
    public int Score;
    public string DateTime;

    public HighScore(string name, int score, DateTime dateTime)
    {
        Name = string.IsNullOrWhiteSpace(name) ? "Anonymous" : name;
        Score = score < 0 ? 0 : score;
        DateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
}