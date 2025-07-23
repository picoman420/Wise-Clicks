using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ------------------------------------------------------
// *** -- FOR QUIZ: Attached to all Choice button Prefab -- ***
// ------------------------------------------------------

public class ChoiceManager : MonoBehaviour
{
    // Public variables
    public QuizData quizDataScript;  // reference Quiz data script to access data

    // Display wrong UI
    public Transform positionUI;
    public GameObject wrongUI;

    // Private variables
    private Transform selectedChoiceText;


    // Determine if wrong button clicked regardless of randomization
    public void ChoiceClicked()
    {

        selectedChoiceText = this.gameObject.transform.GetChild(0);  // get first child

        for (int i = 0; i < quizDataScript.choices[quizDataScript.randomNum - 1].Length; i++)
        {
            // Check if choice clicked is wrong
            if (selectedChoiceText.GetComponent<TMP_Text>().text != quizDataScript.choices[quizDataScript.randomNum - 1][0])
            {
                // Wrong Answer clicked
                Instantiate(wrongUI, positionUI);
                break;
            }
        }
    }
}
