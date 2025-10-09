using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    private TouchScreenKeyboard keyboard;

    public void OnLoginClicked()
    {
        if (nameInputField == null)
        {
            Debug.LogError("nameInputField is not assigned in the Inspector!");
            return;
        }

        string playerName = nameInputField.text.Trim();
        if (!string.IsNullOrEmpty(playerName))
        {
            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager.Instance is null! Ensure GameManager is in an initial scene.");
                return;
            }
            GameManager.Instance.SetPlayerName(playerName);
            SceneManager.LoadScene("HomeScene");
        }
        else
        {
            Debug.LogWarning("Please enter a valid name.");
        }
    }

    public void OpenKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, true);
    }
}