using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// ------------------------------------------------------
// *** -- FOR QUIZ: Attached QuizData from Choice button Prefab -- ***
// ------------------------------------------------------

public class QuizManager : MonoBehaviour
{
    // Private variables
    private int tracker;
    private int numWrongAns;
    private Button mcqBtns;

    private List<int> origIndexes = new List<int>();
    private List<int> shuffledIndexes = new List<int>();
    private List<GameObject> existingButtonsList = new List<GameObject>();

    // Public variables
    public QuizData quizDataScript;  // reference Quiz data script to access data

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI qnsCounterText;

    public Transform parentChoices;
    public GameObject choicePrefab;

    public GameObject nextBtn;

    public GameObject completionMenu;
    public TextMeshProUGUI score;


    // Start is called before the first frame update
    void Start()
    {
        InitializeShuffledQns();
        ProceedNextTask(); // Generate first qns at the start
    }

    // Initialize shuffled array on start  eg. [0...5]
    private void InitializeShuffledQns()
    {
        for (int i=0; i < quizDataScript.questions.Length; i++)
        {
            origIndexes.Add(i);
        }

        shuffledIndexes = shuffleChoices(origIndexes);

        Debug.Log(string.Join(", ", shuffledIndexes));
    }

    // To shuffle arrays
    private List<T> shuffleChoices<T>(List<T> choices)
    {
        for (int j=0; j < choices.Count; j++)
        {
            int rand = Random.Range(j, choices.Count);

            T temp = choices[j];
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

            if (child.gameObject.tag == "Wrong")
            {
                numWrongAns += 1;
            }

            DescendantsDefaultState(child, refText);
        }
    }

    // Clear prefabs in list
    private void RemoveExistingButtons(List<GameObject> btnsPrefab)
    {
        foreach (GameObject prefab in btnsPrefab)
        {
            if (prefab != null)
            {
                Destroy(prefab);
            }
        }
        btnsPrefab.Clear();
    }

    // Check for Buttons Clicked
    private void TaskOnClick()
    {
        // Set visibility true for next btn
        nextBtn.SetActive(true);

        ScoreSystem(parentChoices);
        //Debug.Log(numWrongAns);
    }

    // Score System (Search for wrong UI to indicate -- Score will not be saved)
    private void ScoreSystem(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == "Wrong")
            {
                numWrongAns += 1;
            }
            ScoreSystem(child);
        }
    }


    // Randomize + Instantiate Choices buttons from respective Question
    public void ChoicesManager(string[][] choiceArray, int randNum)
    {
        int numChoices = choiceArray[randNum].Length; // the number of items in each array

        // Array of shuffled choices
        List<string> choicesList = choiceArray[randNum].ToList();
        List<string> shuffleChoicesArray = shuffleChoices(choicesList);

        for (int i=0; i < numChoices; i++)  // instantiate no. of times based on given choices
        {
            GameObject choiceBtn = Instantiate(choicePrefab, parentChoices);
            choiceBtn.name = choicePrefab.name + i;  // instantiated name label for identifying

            // Check if buttons r clicked (no matter the order)
            mcqBtns = choiceBtn.GetComponent<Button>();
            mcqBtns.onClick.AddListener(TaskOnClick);

            existingButtonsList.Add(choiceBtn);  // add buttons to list for tracking

            DescendantsDefaultState(choiceBtn.transform, shuffleChoicesArray[i]);
        }
    }

    // Generate New Content
    public void ProceedNextTask()
    {
        if (shuffledIndexes.Count == 0)
        {
            Debug.Log("Quiz completed");
            completionMenu.SetActive(true);

            int correctAns = quizDataScript.questions.Length - numWrongAns;
            score.text = correctAns + " / " + quizDataScript.questions.Length;

            return;
        }

        // Remove first index from shuffled list
        quizDataScript.randomNum = shuffledIndexes[0];
        shuffledIndexes.RemoveAt(0);

        // Clear existing buttons for new ones to populate
        RemoveExistingButtons(existingButtonsList);

        ChoicesManager(quizDataScript.choices, quizDataScript.randomNum);

        // Update qns text
        questionText.text = quizDataScript.questions[quizDataScript.randomNum];

        // Update counter text
        tracker = quizDataScript.questions.Length - shuffledIndexes.Count;
        qnsCounterText.text = tracker.ToString() + " / " + quizDataScript.questions.Length.ToString();

        nextBtn.SetActive(false);
    }

    // Restart functionality
    public void Restart()
    {
        numWrongAns = 0;
        shuffledIndexes.Clear();  // clear shuffled indexes
        completionMenu.SetActive(false);

        InitializeShuffledQns();
        ProceedNextTask();        // Generate new set of questions
    }

    public void ExitToHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
