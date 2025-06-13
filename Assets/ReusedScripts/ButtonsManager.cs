using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class ButtonsManager : MonoBehaviour
{
    public GameObject[] buttonPrefabs;
    public Transform parentButton;

    public GameObject hintBar;
    public GameObject correctBar;
    public GameObject wrongBar;

    public int currentIndex;

    private List<GameObject> activePrefabs = new List<GameObject>(); // Another list for instances

    void Start()
    {
        SpawnMessages();
        InactiveAllBars();
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

        // Re-instantiate all prefabs
        SpawnMessages();

        InactiveAllBars();
    }

    public void InactiveAllBars()
    {
        // Set Active False to bars
        hintBar.SetActive(false);
        correctBar.SetActive(false);
        wrongBar.SetActive(false);
    }
}
