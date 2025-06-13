using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MessageManager : MonoBehaviour
{
    public GameObject currentManager;

    public TextMeshProUGUI hintText;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI wrongText;

    private ButtonsManager buttonsManager;
    private UpdatePoints updatePoints;

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