using UnityEngine;
using UnityEngine.UI;
using TMPro; // Ensure TMPro is included
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public Button changeNameButton;
    public Slider audioSlider;

    void Start()
    {
        audioSlider.onValueChanged.AddListener(OnAudioValueChanged);
    }

    public void BackButtonClick()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void ChangeNameClick()
    {
        // Change current username to new username        
    }

    public void OnAudioValueChanged(float value)
    {
        Debug.Log($"Audio volume set to: {value}");
    }
}