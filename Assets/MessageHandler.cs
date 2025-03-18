using UnityEngine;
using UnityEngine.UI;

public class MessageHandler : MonoBehaviour
{
    public Text messageText;
    public Button scamButton, realButton;
    public GameObject hintPanel;
    public Text hintText;

    private int currentMessageIndex = 0;
    private MessageDatabase messageDatabase;
    private ScoreManager scoreManager; // Reference to ScoreManager

    void Start()
    {
        messageDatabase = MessageDatabase.Instance;
        scoreManager = FindObjectOfType<ScoreManager>(); // Get ScoreManager

        if (messageDatabase == null || messageDatabase.messages == null || messageDatabase.messages.Count == 0)
        {
            Debug.LogError("MessageDatabase is empty or not loaded correctly.");
            return;
        }

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene!");
            return;
        }

        UpdateMessage();

        scamButton.onClick.AddListener(() => CheckMessage(true));
        realButton.onClick.AddListener(() => CheckMessage(false));
    }

    void CheckMessage(bool isScamButtonClicked)
    {
        if (currentMessageIndex >= messageDatabase.messages.Count) return;

        MessageData.Message currentMessage = messageDatabase.messages[currentMessageIndex];

        if (isScamButtonClicked == currentMessage.isScam)
        {
            // ✅ Correct choice: Increase score and unlock the next message
            scoreManager.AddScore();
            NextMessage();
        }
        else
        {
            // ❌ Wrong choice: Show hint & display a new scam message instead of proceeding
            ShowHint(currentMessage.hint);
            DisplayNewScamMessage();
        }
    }

    void ShowHint(string hint)
    {
        hintPanel.SetActive(true);
        hintText.text = hint;
    }

    void NextMessage()
    {
        currentMessageIndex++;
        if (currentMessageIndex < messageDatabase.messages.Count)
        {
            UpdateMessage();
        }
        else
        {
            messageText.text = "Game Over!";
            scamButton.interactable = false;
            realButton.interactable = false;
        }
    }

    void UpdateMessage()
    {
        messageText.text = messageDatabase.messages[currentMessageIndex].text;
    }

    void DisplayNewScamMessage()
    {
        foreach (var msg in messageDatabase.messages)
        {
            if (msg.isScam)  // Find a new scam message
            {
                messageText.text = msg.text;
                hintText.text = msg.hint;
                return;
            }
        }
    }
}
