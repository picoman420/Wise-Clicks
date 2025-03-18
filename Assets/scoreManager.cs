using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;
    private const int maxScore = 50;

    void Start()
    {
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    public void AddScore()
    {
        score += 10;
        UpdateScoreUI();
        CheckLevelProgress();
    }

    public void SubScore()
    {
        score = Mathf.Max(0, score - 10);  // Prevent negative scores
        UpdateScoreUI();
    }

    private void CheckLevelProgress()
    {
        if (score >= maxScore)
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
