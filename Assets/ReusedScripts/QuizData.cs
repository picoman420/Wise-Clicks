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
        "A caller claims to be from your bank and asks for your account password to verify your identity. What should you do?",
        "If your friend asks you for money because of an emergency, what should you do?",
        "When is the ScamShield Helpline available?",
        "Which is the safest action if you receive a suspicious link online?",
        "If an email has the company’s official logo, it must be safe.",
        "Banks / Government agencies will NEVER ask you to transfer money.",
        "Scammers only target older people who aren’t tech-savvy.",
        "Banks / Government agencies will transfer your call to Police.",
        "Your friend sent you a link to a 'viral video of you'. What do you do?"
    };

    // ****  Choices Available ("1st" (index 0) in array is CORRECT answer)  ****
    public string[][] choices =
    {
        new string[] {"1799", "1010", "1999", "999"},
        new string[] {"Ignore and contact bank directly", "Give password immediately", "Ask for confirmation email", "Check their identification (ID)"},
        new string[] {"Call them directly to confirm", "Send the money", "Share to your friends", "Ask for their bank account details"},
        new string[] {"24 / 7, Everyday", "24 / 7, Weekdays Only", "9am - 11pm, Everyday", "8am - 11pm, Weekdays Only" },
        new string[] { "Delete the message without clicking", "Click the link to see what it is", "Forward the message to friends", "Reply the sender" },
        new string[] {"False", "True"},
        new string[] { "True", "False"},
        new string[] {"False (Anyone can be targeted.)", "True"},
        new string[] {"False", "True"},
        new string[] { "Call them directly to ask", "Click the link", "Reply and ask what the video is", "Share the link with others" },
    };

    // For getting index of qns + ans
    public int randomNum;
}
