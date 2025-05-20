using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    public GameObject exitPanel; // Assign in Inspector
    public GameObject infoPanel; // Assign in Inspector

    void Start()
    {
        // Navigation buttons
        GameObject newGameButton = GameObject.Find("NewGameButton");
        if (newGameButton != null)
        {
            newGameButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("LevelScene"));
        }

        GameObject leaderboardButton = GameObject.Find("LeaderboardButton");
        if (leaderboardButton != null)
        {
            leaderboardButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("LeaderboardScene"));
        }

        GameObject settingsButton = GameObject.Find("SettingsButton");
        if (settingsButton != null)
        {
            settingsButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("SettingsScene"));
        }

        GameObject backButton = GameObject.Find("BackButton");
        if (backButton != null)
        {
            backButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("StartScene"));
        }

        // Bottom buttons
        GameObject infoButton = GameObject.Find("InfoButton");
        if (infoButton != null)
        {
            infoButton.GetComponent<Button>().onClick.AddListener(OpenInfoPanel);
        }

        GameObject exitButton = GameObject.Find("ExitButton");
        if (exitButton != null)
        {
            exitButton.GetComponent<Button>().onClick.AddListener(OpenExitPanel);
        }

        // Ensure panels are hidden at start
        if (exitPanel != null) exitPanel.SetActive(false);
        if (infoPanel != null) infoPanel.SetActive(false);
    }

    void OpenExitPanel()
    {
        if (exitPanel != null)
        {
            exitPanel.SetActive(true);
            infoPanel?.SetActive(false); // Hide other panel if open
        }
    }

    void OpenInfoPanel()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(true);
            exitPanel?.SetActive(false); // Hide other panel if open
        }
    }

    // Called by Yes button in ExitPanel
    public void ConfirmExit()
    {
        Application.Quit();
    }

    // Called by No button in ExitPanel or Close button in InfoPanel
    public void ClosePanel(GameObject panel)
    {
        if (panel != null && panel.activeSelf)
        {
            panel.SetActive(false);
        }
    }

    // Alternative methods for direct panel closure (optional)
    public void CloseExitPanel()
    {
        ClosePanel(exitPanel);
    }

    public void CloseInfoPanel()
    {
        ClosePanel(infoPanel);
    }
}