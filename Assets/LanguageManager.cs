using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageManager : MonoBehaviour
{
    private string selectedLanguage = "English";

    public void LoadLoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }

    public void SelectLanguage(string language)
    {
        selectedLanguage = language;
        Debug.Log("Selected language: " + selectedLanguage);
        // Note: Language selection will be stored or used later for localization
    }

    public void EnglishLanguage()
    {
        SelectLanguage("English");
    }

    public void ChineseLanguage()
    {
        SelectLanguage("中文");
    }

    public void MalayLanguage()
    {
        SelectLanguage("Melayu");
    }

}