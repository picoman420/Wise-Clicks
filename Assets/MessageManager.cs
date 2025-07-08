using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

// Managing message scene
public class MessageManager : MonoBehaviour
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
        "Check if domain link is legitimate.",
        "If it sounds too good to be true, it probably is.",
        "Spelling or grammatical errors are a giveaway.",
        "If it sounds too good to be true, it probably is.",
        "Any action required from your end?",
        "Be wary of suspicious or unusual website links.",
        "Any suspicious website links provided?",
        "If it sounds too good to be true, it probably is.",
        "If it sounds too good to be true, it probably is.",
        "Check if domain link is legitimate.",
        "If it sounds too good to be true, it probably is.",
    };

    // for correct section
    private string[] corrects = {
        "Correct! This is a SCAM.",
        "Correct! This is a REAL message.",
        "Correct! This is a SCAM.",
        "Correct! This is a SCAM.",
        "Correct! This is a SCAM.",
        "Correct! This is a REAL message.",
        "Correct! This is a SCAM.",
        "Correct! This is a REAL message.",
        "Correct! This is a SCAM.",
        "Correct! This is a SCAM.",
        "Correct! This is a REAL message.",
        "Correct! This is a SCAM.",
    };

    // for wrong section
    private string[] wrongs = {
        "Incorrect! This is a SCAM.",
        "Incorrect! This is a REAL message by MOH.",
        "Incorrect! This is a SCAM.",
        "Incorrect! This is a SCAM.",
        "Incorrect! This is a SCAM.",
        "Incorrect! This is a REAL message.",
        "Incorrect! This is a SCAM.",
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
    }

    public void OnHintClicked()
    {
        buttonsManager.hintBar.SetActive(true);
        hintText.text = hints[buttonsManager.currentIndex];
    }

    public void CorrectAnsClicked()
    {
        buttonsManager.correctBar.SetActive(true);
        correctText.text = corrects[buttonsManager.currentIndex];

        updatePoints.OnLegitOrScamClicked(true);

        // text change
        buttonsManager.ChangeText(parentPopup, corrects[buttonsManager.currentIndex]);

        // color change
        buttonsManager.ChangeColors(parentPopup, true);
    }

    public void WrongAnsClicked()
    {
        buttonsManager.wrongBar.SetActive(true);
        wrongText.text = wrongs[buttonsManager.currentIndex];

        updatePoints.OnLegitOrScamClicked(false);

        // text change
        buttonsManager.ChangeText(parentPopup, wrongs[buttonsManager.currentIndex]);

        // color change
        buttonsManager.ChangeColors(parentPopup, false);
    }
}