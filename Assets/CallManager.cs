using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CallManager : MonoBehaviour
{
    public GameObject currentManager; // attached gameobject containing the script
    public Transform parentPopup; // to change border color of info section

    public TextMeshProUGUI correctText;
    public TextMeshProUGUI wrongText;

    private ButtonsManager buttonsManager; // reference instance of the script
    private UpdatePoints updatePoints; // reference instance of the script

    // for correct section
    private string[] corrects = {
        "Correct! This is a SCAM.",
    };

    // for wrong section
    private string[] wrongs = {
        "Incorrect! This is a SCAM.",
    };

    void Start()
    {
        buttonsManager = currentManager.GetComponent<ButtonsManager>();
        updatePoints = currentManager.GetComponent<UpdatePoints>();
        if (buttonsManager == null || updatePoints == null)
        {
            Debug.LogError("ButtonsManager or UpdatePoints component not found on " + currentManager.name);
        }
    }

    public void CorrectAnsClicked()
    {
        if (buttonsManager != null && correctText != null)
        {
            buttonsManager.correctBar.SetActive(true);
            correctText.text = corrects[buttonsManager.currentIndex];

            if (updatePoints != null)
            {
                updatePoints.OnLegitOrScamClicked(true);
            }

            // text change
            buttonsManager.ChangeText(parentPopup, corrects[buttonsManager.currentIndex]);

            // color change
            buttonsManager.ChangeColors(parentPopup, true);
        }
    }

    public void WrongAnsClicked()
    {
        if (buttonsManager != null && wrongText != null)
        {
            buttonsManager.wrongBar.SetActive(true);
            wrongText.text = wrongs[buttonsManager.currentIndex];

            if (updatePoints != null)
            {
                updatePoints.OnLegitOrScamClicked(false);
            }

            // text change
            buttonsManager.ChangeText(parentPopup, wrongs[buttonsManager.currentIndex]);

            // color change
            buttonsManager.ChangeColors(parentPopup, false);
        }
    }
}
