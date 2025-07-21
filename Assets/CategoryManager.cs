using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CategoryManager : MonoBehaviour
{
    private string scene;

    public GameObject gameGuide;

    //public GameObject quizGuide;

    void Start()
    {
        gameGuide.SetActive(false);
        //quizGuide.SetActive(false);
    }

    public void LoadScene()
    {
        if (string.IsNullOrWhiteSpace(scene))
        {
            Debug.Log("Please select a level!");
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void LoadMenu()
    {
        if (scene == "LevelScene") // Selected Game 
        {
            gameGuide.SetActive(true);
        }

        //if (scene == "") // Selected Quiz 
        //{
        //    quizGuide.SetActive(true);
        //}
    }

    public void GoQuiz()
    {
        scene = "";
    }

    public void GoGame()
    {
        scene = "LevelScene";
    }

    public void GoHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
