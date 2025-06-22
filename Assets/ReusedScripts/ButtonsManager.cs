using UnityEngine;
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
        // check if all buttons are pressed, if yes, level completed
        if (counter == buttonPrefabs.Length)
        {
            CompletionGame();
        }
    }

    public void DestroyBtn() // button clicked
    {
        GameObject currentBtn = EventSystem.current.currentSelectedGameObject; // Identify button clicked

        if (activePrefabs.Contains(currentBtn))
        {
            currentIndex = activePrefabs.IndexOf(currentBtn);
            //activePrefabs.Remove(currentBtn); // Removing from instances list
            Destroy(currentBtn);  // Destroy in game
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
        // (to check if qns are all completed)
        if (shouldCount == false)
        {
            // counter should not increment because restart/start happened
            // Set counter back to original
            counter = 0;
        }
        else
        {
            // counter should increment because qns completed
            counter += 1;
        }

        // Set Active False to bars
        hintBar.SetActive(false);
        correctBar.SetActive(false);
        wrongBar.SetActive(false);                
    }

    // Restart Functionality
    public void Restart()
    {
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

        // -- To placed in respective code section -- //
        // -- Need a check to identify which level -- //

        fullStars.SetActive(true);
        //halfStars.SetActive(true);
        //oneStar.SetActive(true);
        //noStar.SetActive(true);

        // -- To update score accordingly -- //

        score.text = "SCORE: " + "00000";
    }

    public void ExitToHome()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void ExitToLevelMap()
    {
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
}
