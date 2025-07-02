using UnityEngine;
using UnityEngine.UI;
using TMPro; // Ensure TMPro is included
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public Button changeNameButton;
    public Slider audioSlider;
    public TMP_InputField nameInputField; // Input field to display and edit the name

    void Start()
    {
        audioSlider.onValueChanged.AddListener(OnAudioValueChanged);
        // Populate the name input field with the current player name
        if (nameInputField != null && GameManager.Instance != null)
        {
            nameInputField.text = GameManager.Instance.GetPlayerName();
        }
        else
        {
            Debug.LogWarning("nameInputField or GameManager.Instance is not assigned!");
        }
    }

    public void BackButtonClick()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void ChangeNameClick()
    {
        if (nameInputField != null && GameManager.Instance != null)
        {
            string newName = nameInputField.text.Trim();
            if (!string.IsNullOrEmpty(newName))
            {
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
    }

    public void OnAudioValueChanged(float value)
    {
        Debug.Log($"Audio volume set to: {value}");
    }
}