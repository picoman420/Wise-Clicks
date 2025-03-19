using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public GameObject messageItemPrefab; // MessageItemTemplate prefab
    public Transform messageListPanel; // MessageListPanel transform

    void Start()
    {
        SpawnMessages();
    }

    void SpawnMessages()
    {
        // Example messages (expand this list later)
        string[] senders = {
            "Bank Alert: Click to verify", // Scam
            "Jane: How are you?",          // Legit
            "Win $1000! Click here",      // Scam
            "Doctor: Appointment reminder" // Legit
        };
        bool[] isScam = { true, false, true, false };

        for (int i = 0; i < senders.Length; i++)
        {
            GameObject message = Instantiate(messageItemPrefab, messageListPanel);
            MessageItem messageItem = message.GetComponent<MessageItem>();
            messageItem.Setup(senders[i], isScam[i]);
        }
    }
}