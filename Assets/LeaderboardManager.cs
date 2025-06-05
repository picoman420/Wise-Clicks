using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI firstPlace;
    public TextMeshProUGUI secondPlace;
    public TextMeshProUGUI thirdPlace;
    //public TextMeshProUGUI playerNameText; // Add this for displaying player's name
    public Transform contentPanel;
    public GameObject entryPrefab;

    void Start()
    {
        // Display player's name
        //if (playerNameText != null)
        //{
        //    string playerName = GameManager.Instance.GetPlayerName();
        //    playerNameText.text = $"Player: {playerName}";
        //}

        // Load and display leaderboard
        DisplayLeaderboard();
    }

    void DisplayLeaderboard()
    {
        List<LeaderboardEntry> leaderboard = GameManager.Instance.LoadLeaderboard();
        string currentPlayer = GameManager.Instance.GetPlayerName();

        // Clear existing entries
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Populate top 3
        // To use transform.GetChild(0).... (to edit the respective points)
        if (leaderboard.Count > 0)
        {
            firstPlace.text = $"1\n{leaderboard[0].name}\n★ {leaderboard[0].score}";
            UpdateTextMaterial(firstPlace, leaderboard[0].name == currentPlayer);
        }
        if (leaderboard.Count > 1)
        {
            secondPlace.text = $"2\n{leaderboard[1].name}\n★ {leaderboard[1].score}";
            UpdateTextMaterial(secondPlace, leaderboard[1].name == currentPlayer);
        }
        if (leaderboard.Count > 2)
        {
            thirdPlace.text = $"3\n{leaderboard[2].name}\n★ {leaderboard[2].score}";
            UpdateTextMaterial(thirdPlace, leaderboard[2].name == currentPlayer);
        }

        // Populate remaining entries
        for (int i = 3; i < leaderboard.Count && i < 5; i++) // Top 5 to match maxLeaderboardEntries
        {
            GameObject entry = Instantiate(entryPrefab, contentPanel);
            TextMeshProUGUI entryText = entry.GetComponent<TextMeshProUGUI>();
            if (entryText != null)
            {
                entryText.text = $"{i + 1}\n{leaderboard[i].name}\n★ {leaderboard[i].score}";
                UpdateTextMaterial(entryText, leaderboard[i].name == currentPlayer);
            }
        }
    }

    void UpdateTextMaterial(TextMeshProUGUI text, bool isCurrentPlayer)
    {
        if (text != null)
        {
            text.enabled = false;
            text.enabled = true;
            if (isCurrentPlayer)
            {
                text.color = Color.green; // Highlight the current player's entry
            }
            else
            {
                text.color = Color.black; // Reset to default color
            }
        }
    }

    public void BackButtonClick()
    {
        SceneManager.LoadScene("HomeScene");
    }
}