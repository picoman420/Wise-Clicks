using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ------------------------------------------------------
// *** -- FOR QUIZ: Attached to all Choice button Prefab -- ***
// ------------------------------------------------------

public class QuizData : MonoBehaviour
{
    // List of questions
    public string[] questions =
    {
        "What helpline number should you call when you're unsure if it's a scam?",
        "What will Government Officials NEVER do over a phone call?",
        "Which of the following is TRUE?",
        "Which one is the REAL ScamShield website link?",
        "When is the ScamShield Helpline available?",
    };

    // ****  Choices Available ("1st" (index 0) in array is CORRECT answer)  ****
    public string[][] choices =
    {
        new string[] {"1799", "1010", "1999", "999"},
        new string[] {"apple", "banana", "pear", "orange"},
        new string[] {"correct", "1", "2", "3"},
        new string[] {"Tokyo", "Sydney"},
        new string[] {"24 / 7, Everyday", "24 / 7, Weekdays Only", "9am - 11pm, Everyday", "8am - 11pm, Weekdays Only" },
    };

    // For getting index of qns + ans
    public int randomNum;
}
