using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MessageData", menuName = "Game/Message Data")]
public class MessageData : ScriptableObject
{
    [System.Serializable]
    public class Message
    {
        public string text;
        public bool isScam; // True = Scam, False = Real
        public string hint;
    }

    public List<Message> messages = new List<Message>();
}
