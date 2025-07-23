using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ------------------------------------------------------
// *** -- FOR QUIZ: Attached QuizData from Choice button Prefab -- ***
// ------------------------------------------------------

public class QuizManager : MonoBehaviour
{
    // Private variables
    private int tracker = 0;

    // Public variables
    public QuizData quizDataScript;  // reference Quiz data script to access data

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI qnsCounterText;

    public Transform parentChoices;
    public GameObject choicePrefab;


    // Start is called before the first frame update
    void Start()
    {
        ProceedNextTask(); // Generate first qns at the start
    }

    // Update is called once per frame
    void Update()
    {

    }

    // To shuffle arrays
    private string[] shuffleChoices(string[] choices)
    {
        for (int j=0; j < choices.Length; j++)
        {
            string temp = choices[j];

            int rand = Random.Range(j, choices.Length);

            choices[j] = choices[rand];
            choices[rand] = temp;
        }

        return choices; // return shuffled array
    }

    // Find descendant to change texts
    private void DescendantsDefaultState(Transform parent, string refText)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == "ChoiceAns")
            {
                child.gameObject.GetComponent<TMP_Text>().text = refText;
            }

            DescendantsDefaultState(child, refText);
        }
    }


    // Randomize + Instantiate Choices buttons from respective Question
    public void ChoicesManager(string[][] choiceArray, int randNum)
    {
        int numChoices = choiceArray[randNum].Length; // the number of items in each array

        // Array of shuffled choices
        string[] shuffleChoicesArray = shuffleChoices(choiceArray[randNum]);

        for (int i=0; i < numChoices; i++)  // instantiate no. of times based on given choices
        {
            GameObject choiceBtn = Instantiate(choicePrefab, parentChoices);

            //if (shuffleChoicesArray[i] == origData.choices[randNum][0])
            //{
            //    quizDataScript.indexCorrectPos = i;
            //}

            DescendantsDefaultState(choiceBtn.transform, shuffleChoicesArray[i]);
        }
    }

    // Generate New Content
    public void ProceedNextTask()
    {
        tracker += 1;

        if (tracker == quizDataScript.questions.Length + 1)
        {
            Debug.Log("Quiz completed");
            return;
        }

        quizDataScript.RandomNumGenerator();
        ChoicesManager(quizDataScript.choices, quizDataScript.randomNum - 1);

        questionText.text = quizDataScript.questions[quizDataScript.randomNum - 1];  // Update qns text
        qnsCounterText.text = tracker.ToString() + " / " + quizDataScript.questions.Length.ToString();

    }
}
