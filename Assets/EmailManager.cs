using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class EmailManager : MonoBehaviour
{
    public GameObject currentManager; // attached gameobject containing the script
    public Transform parentPopup; // to change border color of info section

    public TextMeshProUGUI hintText;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI wrongText;

    private ButtonsManager buttonsManager; // reference instance of the script
    private UpdatePoints updatePoints; // reference instance of the script

    // Data texts for output
    // for hint section
    private string[] hints = {
        "If it sounds too good to be true, it probably is.",
        "If it sounds too good to be true, it probably is.",
        "Check if sender email is legitimate.",
        "If it sounds too good to be true, it probably is.",
        "Any sensitive personal information asked?",
        "Check if sender email is legitimate.",
        "Spelling or grammatical errors are a giveaway.",
        "Any suspicious website links provided?",
        "Check if sender email is legitimate.",
        "Any sensitive personal information asked?",
    };

    // for correct section
    private string[] corrects = {
        "Correct! This is a SCAM.",
        "Correct! This is a SCAM.",
        "Correct! This is a REAL message.",
        "Correct! This is a SCAM.",
        "Correct! This is a REAL message.",
        "Correct! This is a REAL message.",
        "Correct! This is a SCAM.",
        "Correct! This is a SCAM.",
        "Correct! This is a REAL message.",
        "Correct! This is a SCAM.",
    };

    // for wrong section
    private string[] wrongs = {
        "Incorrect! This is a SCAM.",
        "Incorrect! This is a SCAM.",
        "Incorrect! This is a REAL message.",
        "Incorrect! This is a SCAM.",
        "Incorrect! This is a REAL message.",
        "Incorrect! This is a REAL message.",
        "Incorrect! This is a SCAM.",
        "Incorrect! This is a SCAM.",
        "Incorrect! This is a REAL message.",
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

    public void OnHintClicked()
    {
        if (buttonsManager != null && hintText != null)
        {
            buttonsManager.hintBar.SetActive(true);
            hintText.text = hints[buttonsManager.currentIndex];
        }
    }

    public void CorrectAnsClicked()
    {
        if (buttonsManager != null && correctText != null)
        {
            buttonsManager.correctBar.SetActive(true);
            correctText.text = corrects[buttonsManager.currentIndex];

            // text change
            buttonsManager.ChangeText(parentPopup, corrects[buttonsManager.currentIndex]);

            if (updatePoints != null)
            {
                updatePoints.OnLegitOrScamClicked(true);
            }
        }

        // color change
        buttonsManager.ChangeColors(parentPopup, true);
    }

    public void WrongAnsClicked()
    {
        if (buttonsManager != null && wrongText != null)
        {
            buttonsManager.wrongBar.SetActive(true);
            wrongText.text = wrongs[buttonsManager.currentIndex];

            // text change
            buttonsManager.ChangeText(parentPopup, wrongs[buttonsManager.currentIndex]);

            if (updatePoints != null)
            {
                updatePoints.OnLegitOrScamClicked(false);
            }
        }

        // color change
        buttonsManager.ChangeColors(parentPopup, false);
    }
}