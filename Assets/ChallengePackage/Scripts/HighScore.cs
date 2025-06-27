[System.Serializable]
public class HighScore
{
    public string Name;
    public int Score;

    public HighScore(string name, int score)
    {
        Name = string.IsNullOrEmpty(name) ? "Anonymous" : name;
        Score = score < 0 ? 0 : score;
    }
}