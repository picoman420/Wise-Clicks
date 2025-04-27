using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text balanceText; // Reference to BalanceText UI
    private int accountBalance = 1000;

    private const int maxLeaderboardEntries = 5;
    private const string leaderboardKey = "Leaderboard";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateBalanceUI();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignBalanceText();
        UpdateBalanceUI();
    }

    // New method to assign balanceText
    void AssignBalanceText()
    {
        GameObject balanceTextObj = GameObject.Find("BalanceText");
        if (balanceTextObj != null)
        {
            balanceText = balanceTextObj.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogWarning("No BalanceText found in scene: " + SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateBalance(int amount)
    {
        accountBalance += amount;
        UpdateBalanceUI();
        if (accountBalance <= 0)
        {
            Debug.Log("Game Over! Balance reached zero.");
        }
    }

    void UpdateBalanceUI()
    {
        // If balanceText is null, try to find it
        if (balanceText == null)
        {
            AssignBalanceText();
        }

        // Only update if balanceText is valid
        if (balanceText != null)
        {
            balanceText.text = "$" + accountBalance;
        }
        else
        {
            Debug.LogWarning("Cannot update balance UI: balanceText is still null.");
        }
    }
    public void SaveScore(string playerName, int score)
    {
        List<LeaderboardEntry> leaderboard = LoadLeaderboard();
        leaderboard.Add(new LeaderboardEntry { name = playerName, score = score });
        leaderboard.Sort((a, b) => b.score.CompareTo(a.score)); // Sort descending
        if (leaderboard.Count > maxLeaderboardEntries)
        {
            leaderboard.RemoveAt(leaderboard.Count - 1); // Keep top 5
        }

        // Save to PlayerPrefs
        for (int i = 0; i < leaderboard.Count; i++)
        {
            PlayerPrefs.SetString($"{leaderboardKey}_{i}_name", leaderboard[i].name);
            PlayerPrefs.SetInt($"{leaderboardKey}_{i}_score", leaderboard[i].score);
        }
        PlayerPrefs.SetInt($"{leaderboardKey}_count", leaderboard.Count);
        PlayerPrefs.Save();
    }

    // Load the leaderboard
    public List<LeaderboardEntry> LoadLeaderboard()
    {
        List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
        int count = PlayerPrefs.GetInt($"{leaderboardKey}_count", 0);
        for (int i = 0; i < count; i++)
        {
            string name = PlayerPrefs.GetString($"{leaderboardKey}_{i}_name", "Unknown");
            int score = PlayerPrefs.GetInt($"{leaderboardKey}_{i}_score", 0);
            leaderboard.Add(new LeaderboardEntry { name = name, score = score });
        }
        return leaderboard;
    }
}

// Struct to hold leaderboard data
[System.Serializable]
public struct LeaderboardEntry
{
    public string name;
    public int score;
}
