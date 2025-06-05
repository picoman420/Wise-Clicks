using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void LoadLanguageScene()
    {
        SceneManager.LoadScene("LanguageScene");
    }
}