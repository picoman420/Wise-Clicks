using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI; // Added this line to resolve Button reference

public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderboardPanel; // Leaderboard UI panel
    public TMP_Text entryTemplate; // Template for leaderboard entries
    public GameObject closeButton; // Close button for the panel

    private List<TMP_Text> entryTexts = new List<TMP_Text>();

    void Start()
    {
        // Hide panel initially
        leaderboardPanel.SetActive(false);
        entryTemplate.gameObject.SetActive(false); // Hide template

        // Add button listeners
        closeButton.GetComponent<Button>().onClick.AddListener(HideLeaderboard);
    }

    public void ShowLeaderboard()
    {
        // Clear previous entries
        foreach (var entry in entryTexts)
        {
            Destroy(entry.gameObject);
        }
        entryTexts.Clear();

        // Load and display leaderboard
        List<LeaderboardEntry> leaderboard = GameManager.Instance.LoadLeaderboard();
        leaderboard.Sort((a, b) => b.score.CompareTo(a.score)); // Sort descending

        for (int i = 0; i < leaderboard.Count; i++)
        {
            TMP_Text newEntry = Instantiate(entryTemplate, entryTemplate.transform.parent);
            newEntry.text = $"{i + 1}. {leaderboard[i].name} - ${leaderboard[i].score}";
            newEntry.gameObject.SetActive(true);
            entryTexts.Add(newEntry);

            // Force layout rebuild
            LayoutRebuilder.ForceRebuildLayoutImmediate(newEntry.GetComponent<RectTransform>().parent.GetComponent<RectTransform>());
        }

        leaderboardPanel.SetActive(true);
    }

    public void HideLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }
}