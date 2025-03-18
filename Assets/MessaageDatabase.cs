using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MessageDatabase", menuName = "Game/Message Database")]
public class MessageDatabase : ScriptableObject
{
    public List<MessageData.Message> messages;  // Store scam and real messages

    private static MessageDatabase _instance;

    public static MessageDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<MessageDatabase>("MessageDatabase");
                if (_instance == null)
                {
                    Debug.LogError("MessageDatabase.asset not found! Ensure it's inside the 'Resources' folder.");
                }
            }
            return _instance;
        }
    }
}
