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
        // Create name input panel
        GameObject nameInputPanel = new GameObject("NameInputPanel");
        nameInputPanel.AddComponent<CanvasRenderer>();
        RectTransform rectTransform = nameInputPanel.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(300, 150);
        rectTransform.position = new Vector2(Screen.width / 2, Screen.height / 2);

        TMP_InputField inputField = nameInputPanel.AddComponent<TMP_InputField>();
        inputField.textComponent = nameInputPanel.AddComponent<TextMeshProUGUI>();
        inputField.placeholder = nameInputPanel.AddComponent<TextMeshProUGUI>();
        //inputField.placeholder.text = "Enter new name";
        inputField.contentType = TMP_InputField.ContentType.Name;

        // Create Confirm Button with TextMeshProUGUI
        GameObject confirmObj = new GameObject("ConfirmButton", typeof(RectTransform));
        confirmObj.transform.SetParent(nameInputPanel.transform, false);
        Button confirmButton = confirmObj.AddComponent<Button>();
        rectTransform = confirmObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, -50);
        rectTransform.sizeDelta = new Vector2(100, 50);

        GameObject confirmTextObj = new GameObject("Text", typeof(RectTransform));
        confirmTextObj.transform.SetParent(confirmObj.transform, false);
        TextMeshProUGUI confirmText = confirmTextObj.AddComponent<TextMeshProUGUI>();
        confirmText.text = "Confirm";
        confirmText.alignment = TextAlignmentOptions.Center;
        confirmButton.targetGraphic = confirmText;

        confirmButton.onClick.AddListener(() =>
        {
            string newName = inputField.text.Trim();
            if (!string.IsNullOrEmpty(newName))
            {
                GameManager.Instance.SetPlayerName(newName);
                Destroy(nameInputPanel);

            }
        });

        // Create Close Button with TextMeshProUGUI
        GameObject closeObj = new GameObject("CloseButton", typeof(RectTransform));
        closeObj.transform.SetParent(nameInputPanel.transform, false);
        Button closeButton = closeObj.AddComponent<Button>();
        rectTransform = closeObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(100, -50);
        rectTransform.sizeDelta = new Vector2(50, 50);

        GameObject closeTextObj = new GameObject("Text", typeof(RectTransform));
        closeTextObj.transform.SetParent(closeObj.transform, false);
        TextMeshProUGUI closeText = closeTextObj.AddComponent<TextMeshProUGUI>();
        closeText.text = "X";
        closeText.alignment = TextAlignmentOptions.Center;
        closeButton.targetGraphic = closeText;

        closeButton.onClick.AddListener(() => Destroy(nameInputPanel));
    }

    public void OnAudioValueChanged(float value)
    {
        Debug.Log($"Audio volume set to: {value}");
    }
}