using UnityEngine;
using UnityEngine.UI;
using TMPro; // Ensure TMPro is included
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public Button changeNameButton;
    public Slider audioSlider;
    public TMP_Text currentUsername;
    public TMP_InputField newUsername;  // Input field to display and edit the name

    private TouchScreenKeyboard keyboard;

    void Start()
    {
        audioSlider.onValueChanged.AddListener(OnAudioValueChanged);
        // Populate the name input field with the current player name
        if (currentUsername != null && GameManager.Instance != null)
        {
            currentUsername.text = GameManager.Instance.GetPlayerName();
        }
        else
        {
            Debug.LogWarning("nameInputField or GameManager.Instance is not assigned!");
        }
    }

    public void ClearInput()
    {
        newUsername.text = "";
    }

    public void ChangeNameClick()
    {
        if (newUsername != null && GameManager.Instance != null)
        {
            string newName = newUsername.text.Trim();
            if (!string.IsNullOrEmpty(newName))
            {
                currentUsername.text = newName; 
                GameManager.Instance.SetPlayerName(newName);
                Debug.Log($"Player name updated to: {newName}");
            }
            else
            {
                Debug.LogWarning("Please enter a valid name.");
            }
        }
        else
        {
            Debug.LogError("nameInputField or GameManager.Instance is null!");
        }

        ClearInput();
    }

    public void OnAudioValueChanged(float value)
    {
        Debug.Log($"Audio volume set to: {value}");
    }

    public void BackButtonClick()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void OpenKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, true);
    }
}