using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour
{
    public Button[] messageButtons; // Assign Message Buttons in Inspector
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < messageButtons.Length; i++)
        {
            messageButtons[i].interactable = false;
        }
    }
    public void UnlockNextButton()
    {
        if (currentIndex < messageButtons.Length - 1)
        {
            currentIndex++;
            messageButtons[currentIndex].interactable = true; // Enable next button
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
