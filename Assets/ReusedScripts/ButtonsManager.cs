using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// ------------------------------------------------------
// *** -- A REUSED Manager script for ALL Templates! -- ***
// ------------------------------------------------------

public class ButtonsManager : MonoBehaviour
{
    // Managing prefabs 
    public GameObject[] buttonPrefabs;
    public Transform parentButton;

    // Set visibility for top bars
    public GameObject hintBar;
    public GameObject correctBar;
    public GameObject wrongBar;

    // Managing popups during restart
    public GameObject navButtonsContent;
    public Transform parentPopup;

    // Completion Menu
    public GameObject completionMenu;
    public GameObject fullStars;
    public GameObject halfStars;
    public GameObject oneStar;
    public GameObject noStar;
    public TextMeshProUGUI score;

    // Data for execution in code
    public int currentIndex;

    private List<GameObject> activePrefabs = new List<GameObject>(); // Another list for instances
    private int counter = 0; // for checking if all qns are answered

    void Start()
    {
        SpawnMessages();
        ProceedNextTaskReset(false);

        // Set visibility False for completion menu
        ManagingCompletionMenu(false);
    }

    void Update()
    {
        // Check if all buttons are pressed and tasks completed
        // Debug.Log($"Counter: {counter}, ButtonPrefabs Length: {buttonPrefabs.Length}");
        if (counter == buttonPrefabs.Length)
        {
            CompletionGame();
        }
    }

    public void DestroyBtn() // button clicked
    {
        GameObject currentBtn = EventSystem.current.currentSelectedGameObject; // Identify button clicked

        if (currentBtn != null && activePrefabs.Contains(currentBtn))
        {
            currentIndex = activePrefabs.IndexOf(currentBtn);
            Destroy(currentBtn);  // Destroy in game
            Debug.Log($"Destroyed main button at index {currentIndex}, Counter: {counter}");
        }
        else
        {
            Debug.LogWarning("Current selected button is null or not in active list!");
        }
    }

    public void SpawnMessages()
    {
        foreach (GameObject prefab in buttonPrefabs)
        {
            GameObject buttons = Instantiate(prefab, parentButton);
            buttons.SetActive(true);
            activePrefabs.Add(buttons);
        }
    }

    public void ProceedNextTaskReset(bool shouldCount)
    {
        // Handle UI resets and increment counter on task completion
        if (shouldCount)
        {
            counter++; // Increment only when OK is clicked after task completion
            Debug.Log($"Task completed, Counter incremented to: {counter}");
        }
        else
        {
            // Reset for restart/start
            counter = 0;
        }

        // Set Active False to bars
        hintBar.SetActive(false);
        correctBar.SetActive(false);
        wrongBar.SetActive(false);                
    }

    // Restart Functionality
    public void Restart()
    {
        // Reset balance to 1000
        if (GameManager.Instance != null)
        {
            int currentBalance = GameManager.Instance.GetAccountBalance();
            GameManager.Instance.UpdateBalance(1000 - currentBalance);
        }

        // Destroy any remaining messages
        foreach (GameObject currentPrefab in activePrefabs)
        {
            if (currentPrefab != null)
                Destroy(currentPrefab);
        }
        activePrefabs.Clear();

        // Restart during popup
        // Set visibility to False for all popups
        foreach (Transform popups in parentPopup)
        {
            DescendantsDefaultState(popups);
            popups.gameObject.SetActive(false);
        }
        navButtonsContent.SetActive(true);

        // Re-instantiate all prefabs
        SpawnMessages();

        // Set visibility False for top bars
        ProceedNextTaskReset(false);

        // Set visibility False for completion menu
        ManagingCompletionMenu(false);
    }

    // Check for completion of game and Set Visibility of number of stars to display
    public void CompletionGame()
    {
        completionMenu.SetActive(true);

        // Save the score before displaying
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveScore();
        }

        // Get current balance as score
        int currentScore = GameManager.Instance != null ? GameManager.Instance.GetAccountBalance() : 1000;

        // Update score text
        if (score != null)
        {
            score.text = "SCORE: " + currentScore.ToString();
        }
        else
        {
            Debug.LogWarning("score TextMeshProUGUI is not assigned!");
        }

        // Calculate and display stars based on score
        CalculateStars(currentScore);
    }

    private void CalculateStars(int score)
    {
        // Reset all star displays
        fullStars.SetActive(false);
        halfStars.SetActive(false);
        oneStar.SetActive(false);
        noStar.SetActive(false);

        // Determine star rating based on score (3-star system)
        if (score >= 1300) // 3 stars (87% of max 1500)
        {
            fullStars.SetActive(true); // 3 stars
        }
        else if (score >= 1000) // 2 stars (67% of max)
        {
            halfStars.SetActive(true); // 2 stars
        }
        else if (score >= 500) // 1 star (33% of max)
        {
            oneStar.SetActive(true); // 1 star
        }
        else // 0 stars (<33% of max)
        {
            noStar.SetActive(true); // 0 stars
        }
    }

    public void ExitToHome()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void ExitToLevelMap()
    {
        if (GameManager.Instance != null)
        {
            int currentScore = GameManager.Instance.GetAccountBalance();
            int stars = 0;
            if (currentScore >= 1300) stars = 3;
            else if (currentScore >= 1000) stars = 2;
            else if (currentScore >= 500) stars = 1;
            else stars = 0;
            GameManager.Instance.SaveLevelStars(SceneManager.GetActiveScene().name, stars); // Save stars for this level
        }
        SceneManager.LoadScene("LevelScene");
    }

    // Find descendants of POPUP with tag to set all to default state when restarted
    private void DescendantsDefaultState(Transform parent)
    {
        // find tag to set back to default state
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == "Button")
            {
                child.gameObject.SetActive(true);
            }

            if (child.gameObject.tag == "Content")
            {
                child.gameObject.SetActive(false);
            }

            if (child.gameObject.tag == "CallAudio")
            {
                child.gameObject.SetActive(true);
            }

            // Recursive loop for find all objects with the tag to set visibility
            DescendantsDefaultState(child);
        }
    }

    private void ManagingCompletionMenu(bool show)
    {
        // Set Active False to stars
        fullStars.SetActive(show);
        halfStars.SetActive(show);
        oneStar.SetActive(show);
        noStar.SetActive(show);

        completionMenu.SetActive(show);
    }

    // Color controller for border
    public void ChangeColors(Transform parent, bool isCorrect)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == "Border")
            {
                if (isCorrect) // if correct
                {
                    child.gameObject.GetComponent<RawImage>().color = new Color(121f / 255, 180f / 255, 38f / 255);
                }
                else // if wrong
                {
                    child.gameObject.GetComponent<RawImage>().color = new Color(193f / 255, 18f / 255, 31f / 255);
                }
                break;
            }

            // Recursive loop to find all objects with tag
            ChangeColors(child, isCorrect);
        }
    }

    // Text Controller
    public void ChangeText(Transform parent, string textToChange)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == "Answer")
            {
                child.gameObject.GetComponent<TMP_Text>().text = textToChange;
                break;
            }

            // Recursive loop to find all objects with tag
            ChangeText(child, textToChange);
        }
    }
}