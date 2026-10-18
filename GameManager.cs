using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 1.5f;
    public float minX = -2f;
    public float maxX = 2f;
    public float spawnY = 6f;

    private float timer;
    private float scoreTimer; // Score uchun alohida vaqt hisoblagich
    public int score = 0;
    public int highScore = 0;

    public Text scoreText;
    public Text highScoreText;
    public GameObject gameOverPanel;

    private bool isGameOver = false;

    void Start()
    {
        score = 0;
        scoreTimer = 0f; // Har safar o'yin boshlanganda score vaqti 0 dan boshlanadi
        Time.timeScale = 1f;
        isGameOver = false;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (isGameOver) return;

        timer += Time.deltaTime;
        scoreTimer += Time.deltaTime; // O'yin ichidagi alohida vaqt o'tishi

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }

        // Score endi Time.time ga emas, o'yin boshidan beri o'tgan scoreTimer'ga asoslanadi
        score = Mathf.RoundToInt(scoreTimer * 10);
        
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        // High Score'ni tekshirish va yangilash
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }

        // Qiyinlashtirish
        if (score > 100 && score < 250)
        {
            spawnInterval = 1.2f;
        }
        else if (score >= 250)
        {
            spawnInterval = 0.8f;
        }
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(randomX, spawnY, 0);
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}