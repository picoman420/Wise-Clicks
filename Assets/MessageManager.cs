using UnityEngine;
using TMPro;
using System.Collections.Generic;

// Managing message scene
public class MessageManager : MonoBehaviour
{
    public GameObject currentManager; // attached gameobject containing the script

    public TextMeshProUGUI hintText;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI wrongText;

    private ButtonsManager buttonsManager; // reference instance of the script
    private UpdatePoints updatePoints; // reference instance of the script

    // Data texts for output
    // for hint section
    private string[] hints = {
        "Qns 1",
        "Qns 2",
        "Qns 3",
        "Qns 4",
        "Qns 5",
        "Qns 6",
        "Qns 7",
        "Qns 8",
        "Qns 9",
        "Qns 10",
        "Qns 11",
        "Qns 12",
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
    }

    public void WrongAnsClicked()
    {
        buttonsManager.wrongBar.SetActive(true);
        wrongText.text = wrongs[buttonsManager.currentIndex];

        updatePoints.OnLegitOrScamClicked(false);
    }
}