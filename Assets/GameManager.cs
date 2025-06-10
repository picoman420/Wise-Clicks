using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text balanceText; // Reference to BalanceText UI (optional)
    private int accountBalance = 1000;
    private string playerName = "Player"; // Default name

    private const int maxLeaderboardEntries = 10;
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
        Debug.Log($"GameManager initialized with playerName: {playerName}");
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignBalanceText();
        UpdateBalanceUI();
        Debug.Log($"Scene loaded: {scene.name}, current playerName: {playerName}");
    }

    void AssignBalanceText()
    {
        GameObject balanceTextObj = GameObject.Find("BalanceText");
        if (balanceTextObj != null)
        {
            balanceText = balanceTextObj.GetComponent<TMP_Text>();
        }
        // No warning here, as BalanceText is optional
    }

    public void SetPlayerName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            playerName = name;
            Debug.Log($"Player name set to: {playerName}");
        }
    }

    public string GetPlayerName()
    {
        return playerName;
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

    public int GetAccountBalance()
    {
        return accountBalance;
    }

    void UpdateBalanceUI()
    {
        if (balanceText != null)
        {
            balanceText.text = accountBalance.ToString();
        }
        // Removed warning for null balanceText to avoid clutter
    }

    public void SaveScore()
    {
        List<LeaderboardEntry> leaderboard = LoadLeaderboard();
        leaderboard.Add(new LeaderboardEntry { name = playerName, score = accountBalance });
        leaderboard.Sort((a, b) => b.score.CompareTo(a.score)); // Sort descending
        if (leaderboard.Count > maxLeaderboardEntries)
        {
            leaderboard.RemoveAt(leaderboard.Count - 1); // Keep top 10
        }

        // Save to PlayerPrefs
        for (int i = 0; i < leaderboard.Count; i++)
        {
            PlayerPrefs.SetString($"{leaderboardKey}_{i}_name", leaderboard[i].name);
            PlayerPrefs.SetInt($"{leaderboardKey}_{i}_score", leaderboard[i].score);
        }
        PlayerPrefs.SetInt($"{leaderboardKey}_count", leaderboard.Count);
        PlayerPrefs.Save();
        Debug.Log($"Score saved for {playerName} with score {accountBalance}");
    }

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
        return leaderboard.OrderByDescending(entry => entry.score).ToList();
    }
}

[System.Serializable]
public struct LeaderboardEntry
{
    public string name;
    public int score;
}