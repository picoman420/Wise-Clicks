using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CategoryManager : MonoBehaviour
{
    private string scene;

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
