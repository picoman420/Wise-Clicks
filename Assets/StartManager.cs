using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    void Start()
    {
        GameObject startButton = GameObject.Find("StartButton");
        if (startButton != null)
        {
            startButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("LanguageScene"));
        }
    }
}