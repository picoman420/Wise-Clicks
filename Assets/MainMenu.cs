using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
        public void BackToPreviousScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
}
