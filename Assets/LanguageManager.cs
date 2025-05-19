using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageManager : MonoBehaviour
{
    private string selectedLanguage = "English";

    void Start()
    {
        GameObject englishButton = GameObject.Find("EnglishButton");
        if (englishButton != null)
        {
            englishButton.GetComponent<Button>().onClick.AddListener(() => SelectLanguage("English"));
            englishButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("LoginScene"));
        }

        GameObject chineseButton = GameObject.Find("ChineseButton");
        if (chineseButton != null)
        {
            chineseButton.GetComponent<Button>().onClick.AddListener(() => SelectLanguage("中文"));
            chineseButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("LoginScene"));
        }

        GameObject malayButton = GameObject.Find("MalayButton");
        if (malayButton != null)
        {
            malayButton.GetComponent<Button>().onClick.AddListener(() => SelectLanguage("Melayu"));
            malayButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("LoginScene"));
        }

    }

    void SelectLanguage(string language)
    {
        selectedLanguage = language;
        Debug.Log("Selected language: " + selectedLanguage);
        // Note: Language selection will be stored or used later for localization
    }
}