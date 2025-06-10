using UnityEngine;
using UnityEngine.UI;
using TMPro; // Added for TextMeshProUGUI
using UnityEngine.SceneManagement;

public class LanguageManager : MonoBehaviour
{
    private string selectedLanguage = "English";
    private Button crossButton;

    void Start()
    {
        bool showCross = PlayerPrefs.GetInt("ShowLanguageCross", 0) == 1;
        PlayerPrefs.SetInt("ShowLanguageCross", 0); // Reset flag after check

        // Create or manage cross button
        if (showCross)
        {
            if (crossButton == null)
            {
                GameObject crossObj = new GameObject("CrossButton", typeof(RectTransform));
                crossObj.transform.SetParent(transform, false);
                crossButton = crossObj.AddComponent<Button>();
                RectTransform rectTransform = crossButton.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(Screen.width - 50, Screen.height - 50);
                rectTransform.sizeDelta = new Vector2(50, 50);

                // Add TextMeshProUGUI as a child for the button label
                GameObject textObj = new GameObject("Text", typeof(RectTransform));
                textObj.transform.SetParent(crossObj.transform, false);
                TextMeshProUGUI crossText = textObj.AddComponent<TextMeshProUGUI>();
                crossText.text = "X";
                crossText.alignment = TextAlignmentOptions.Center;
                crossText.fontSize = 24;
                crossButton.targetGraphic = crossText; // Set as the button's target graphic

                crossButton.onClick.AddListener(CloseLanguageScene);
            }
            crossButton.gameObject.SetActive(true);
        }
        else
        {
            if (crossButton != null)
            {
                crossButton.gameObject.SetActive(false);
            }
        }
    }

    public void LoadLoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }

    public void SelectLanguage(string language)
    {
        selectedLanguage = language;
        Debug.Log("Selected language: " + selectedLanguage);
        PlayerPrefs.SetString("SelectedLanguage", selectedLanguage); // Store for persistence
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

    void CloseLanguageScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }
}