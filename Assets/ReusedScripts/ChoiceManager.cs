using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ------------------------------------------------------
// *** -- FOR QUIZ: Attached to all Choice button Prefab -- ***
// ------------------------------------------------------

public class ChoiceManager : MonoBehaviour
{
    // Public variables
    public QuizData quizDataScript;  // reference Quiz data script to access data

    // Display UI
    public Transform positionUI;
    public GameObject wrongUI;
    public GameObject correctUI;
    public bool clicked;

    // Private variables
    private Transform selectedChoiceText;

    void Update()
    {
        if (clicked == true)
        {
            //selectedChoiceText.parent.parent.name  -->  get the parent containing all the buttons

            DescendantsDefaultState(selectedChoiceText.parent.parent);
            DisableAllButtons(selectedChoiceText.parent.parent);
            clicked = false;
        }
    }

    // Find descendant to check position of correct btn
    void DescendantsDefaultState(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == "ChoiceAns")
            {
                if (child.gameObject.GetComponent<TMP_Text>().text == quizDataScript.choices[quizDataScript.randomNum][0])
                {
                    //child.parent.GetChild(1)  -->  get the position of correct answer 

                    // Display UI
                    Instantiate(correctUI, child.parent.GetChild(1));
                    child.parent.gameObject.GetComponent<Image>().color = new Color(196f / 255, 255f / 255, 197f / 255);  // change color

                    break;
                }
            }
            DescendantsDefaultState(child);  // recursive to find all child
        }
    }

    // Disable all buttons
    void DisableAllButtons(Transform parentBtn)
    {
        foreach(Transform child in parentBtn)
        {
            child.gameObject.GetComponent<Button>().interactable = false;
        }
    }

    // Determine if wrong button clicked regardless of randomization
    public void ChoiceClicked()
    {
        selectedChoiceText = this.gameObject.transform.GetChild(0);  // get first child
        clicked = true;

        // Check if choice clicked is wrong
        if (selectedChoiceText.GetComponent<TMP_Text>().text != quizDataScript.choices[quizDataScript.randomNum][0])
        {
            // Display UI
            Instantiate(wrongUI, positionUI);
            selectedChoiceText.parent.GetComponent<Image>().color = new Color(255f / 255, 196f / 255, 196f / 255);  // change color
        }
    }
}
