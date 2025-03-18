using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public MessageData messageData;
    private List<MessageData.Message> activeMessages;
    
    [Header("UI Elements")]
    public List<Button> messageButtons;
    public TextMeshProUGUI messageText;
    public ScoreManager scoreManager;
    public HintManager hintManager;

    private int currentIndex = 0;

    void Start()
    {
        // Automatically load MessageDatabase if not assigned
        if (messageData == null)
        {
            messageData = Resources.Load<MessageData>("MessageDatabase");

            if (messageData == null)
            {
                Debug.LogError("MessageDatabase not found! Make sure it is inside the 'Resources' folder.");
                return;
            }
        }

        // Automatically find all buttons in the scene if not assigned
        if (messageButtons == null || messageButtons.Count == 0)
        {
            messageButtons = new List<Button>(FindObjectsOfType<Button>());
        }

        // Automatically find TextMeshProUGUI if not assigned
        if (messageText == null)
        {
            messageText = FindObjectOfType<TextMeshProUGUI>();
            if (messageText == null)
            {
                Debug.LogError("No TextMeshProUGUI component found in the scene!");
            }
        }

        // Load messages, shuffle them, and display the first one
        activeMessages = new List<MessageData.Message>(messageData.messages);
        ShuffleMessages();
        DisplayMessage();
    }

    private void ShuffleMessages()
    {
        for (int i = activeMessages.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            var temp = activeMessages[i];
            activeMessages[i] = activeMessages[rand];
            activeMessages[rand] = temp;
        }
    }

    private void DisplayMessage()
    {
        if (currentIndex < activeMessages.Count)
        {
            messageText.text = activeMessages[currentIndex].text;
            hintManager.UpdateHint(activeMessages[currentIndex].hint);
        }
        else
        {
            messageText.text = "No more messages!";
        }
    }

    public void OnUserChoice(bool userChoice)
    {
        if (currentIndex >= activeMessages.Count) return;

        bool correct = (userChoice == activeMessages[currentIndex].isScam);

        if (correct)
        {
            scoreManager.AddScore();
            currentIndex++;
            if (currentIndex < activeMessages.Count)
                DisplayMessage();
            else
                messageText.text = "Game Over! Well done!";
        }
        else
        {
            scoreManager.SubScore();
            messageText.text = GetNewMessage();
        }
    }

    private string GetNewMessage()
    {
        return "Incorrect! Try again with a different message.";
    }
}
