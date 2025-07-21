using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    public GameObject exitPanel;       // Assign in Inspector
    public GameObject infoPanel;       // Assign in Inspector   
    public GameObject mainUIContainer; // Assign in Inspector

    void Start()
    {
        // Ensure panels and overlay are hidden at start, main UI is visible
        if (exitPanel != null) exitPanel.SetActive(false);
        if (infoPanel != null) infoPanel.SetActive(false);
        if (mainUIContainer != null) mainUIContainer.SetActive(true);
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("CategoryScene");
    }

    public void LoadLeaderboardScene()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }



    public void OpenExitPanel()
    {
        if (exitPanel != null)
        {
            exitPanel.SetActive(true);
            infoPanel?.SetActive(false);
            mainUIContainer?.SetActive(false); // Hide main UI
        }
    }

    public void OpenInfoPanel()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(true);
            exitPanel?.SetActive(false);
            mainUIContainer?.SetActive(false); // Hide main UI
        }
    }

    public void ConfirmExit()
    {
        Application.Quit();
    }

    public void ClosePanel(GameObject panel)
    {
        if (panel != null && panel.activeSelf)
        {
            panel.SetActive(false);
            mainUIContainer?.SetActive(true); // Show main UI
        }
    }

    public void CloseExitPanel()
    {
        ClosePanel(exitPanel);
    }

    public void CloseInfoPanel()
    {
        ClosePanel(infoPanel);
    }
}