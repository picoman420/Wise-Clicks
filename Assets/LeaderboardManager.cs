using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI firstPlace;
    public TextMeshProUGUI secondPlace;
    public TextMeshProUGUI thirdPlace;

    public Transform parentPanel;
    public GameObject prefabPanel;

    private GameObject entry;

    void Start()
    {
        // Clear existing entries
        foreach (Transform child in parentPanel)
        {
            Destroy(child.gameObject);
        }

        // Load and display leaderboard
        DisplayLeaderboard();
    }

    void DisplayLeaderboard()
    {
        List<LeaderboardEntry> leaderboard = GameManager.Instance.LoadLeaderboard();
        string currentPlayer = GameManager.Instance.GetPlayerName();

        // Debug the loaded leaderboard
        Debug.Log($"Loaded leaderboard for {currentPlayer}:");
        foreach (var entry in leaderboard)
        {
            Debug.Log($"Name: {entry.name}, Score: {entry.score}");
        }

        // Populate top 3
        if (leaderboard.Count > 0)
        {
            firstPlace.text = leaderboard[0].name;
            GameObject firstPlacePoints = firstPlace.transform.GetChild(0).gameObject;
            firstPlacePoints.GetComponent<TextMeshProUGUI>().text = leaderboard[0].score.ToString();
            UpdateTextMaterial(firstPlace, leaderboard[0].name == currentPlayer);
        }
        if (leaderboard.Count > 1)
        {
            secondPlace.text = leaderboard[1].name;
            GameObject secondPlacePoints = secondPlace.transform.GetChild(0).gameObject;
            secondPlacePoints.GetComponent<TextMeshProUGUI>().text = leaderboard[1].score.ToString();
            UpdateTextMaterial(secondPlace, leaderboard[1].name == currentPlayer);
        }
        if (leaderboard.Count > 2)
        {
            thirdPlace.text = leaderboard[2].name;
            GameObject thirdPlacePoints = thirdPlace.transform.GetChild(0).gameObject;
            thirdPlacePoints.GetComponent<TextMeshProUGUI>().text = leaderboard[2].score.ToString();
            UpdateTextMaterial(thirdPlace, leaderboard[2].name == currentPlayer);
        }

        // Populate remaining entries
        for (int i = 3; i < leaderboard.Count && i < 10; i++) // Updated to maxLeaderboardEntries (10)
        {
            entry = Instantiate(prefabPanel, parentPanel);

            foreach (Transform child in entry.transform)
            {
                if (child.CompareTag("Rank"))
                {
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
                }
                
                if (child.CompareTag("Name"))
                {
                    TextMeshProUGUI nameText = child.gameObject.GetComponent<TextMeshProUGUI>();
                    nameText.text = leaderboard[i].name;
                    UpdateTextMaterial(nameText, leaderboard[i].name == currentPlayer);
                }
                
                if (child.CompareTag("Points"))
                {
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = leaderboard[i].score.ToString();
                }
            }
        }
    }

    void UpdateTextMaterial(TextMeshProUGUI textComp, bool isCurrentPlayer)
    {
        if (textComp != null)
        {
            textComp.enabled = true;
            if (isCurrentPlayer)
            {
                textComp.fontStyle = FontStyles.Underline; // Highlight the current player's entry
            }
        }
    }

    public void BackButtonClick()
    {
        SceneManager.LoadScene("HomeScene");
    }
}