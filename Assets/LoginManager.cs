using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;

    void Start()
    {
        GameObject okButton = GameObject.Find("OKButton");
        if (okButton != null)
        {
            okButton.GetComponent<Button>().onClick.AddListener(OnLoginClicked);
        }

        usernameInput = GameObject.Find("InputPlayerName").GetComponent<TMP_InputField>();
    }

    void OnLoginClicked()
    {
        string username = usernameInput.text.Trim();
        if (!string.IsNullOrEmpty(username))
        {
            // Store username (to be used in GameManager later)
            PlayerPrefs.SetString("PlayerName", username);
            PlayerPrefs.Save();
            SceneManager.LoadScene("HomePage");
        }
        else
        {
            Debug.LogWarning("Username cannot be empty!");
        }
    }
}