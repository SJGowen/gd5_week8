using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    [SerializeField]
    private TMP_Text ScoreText;
    [SerializeField]
    private TMP_Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        BestScoreText.text = BestScore.Instance.GetBestScore(BestScore.Instance.highScore);
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize(); 

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 0.5f, ForceMode.Impulse);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(Ball.gameObject);
            GameOver();
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Exit to main menu
                SceneManager.LoadScene("challenge_menu");
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        Debug.Log($"Game Over! Final Score: {m_Points} for {BestScore.Instance.CurrentPlayerName}.");
        if (m_Points > BestScore.Instance.highScore.Score)
        {
            BestScore.Instance.SaveHighScore(BestScore.Instance.CurrentPlayerName, m_Points);
            BestScoreText.text = $"Best Score : {BestScore.Instance.highScore.Name} : {BestScore.Instance.highScore.Score}";
            Debug.Log($"New high score set: {BestScore.Instance.highScore.Name} with {BestScore.Instance.highScore.Score} points.");
        }

        GameOverText.SetActive(true);
    }
}
