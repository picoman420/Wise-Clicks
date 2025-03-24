using UnityEngine;

public class EmailManager : MonoBehaviour
{
    public GameObject emailItemPrefab; // EmailItemTemplate prefab
    public Transform emailListPanel; // EmailListPanel transform

    void Start()
    {
        SpawnEmails();
    }

    void SpawnEmails()
    {
        // Example emails (expand this list later)
        string[] subjects = {
            "URGENT: Account locked!",      // Scam
            "Your utility bill is due",     // Legit
            "You won a free vacation!",     // Scam
            "Meeting reminder: 2 PM"        // Legit
        };
        bool[] isScam = { true, false, true, false };

        for (int i = 0; i < subjects.Length; i++)
        {
            GameObject email = Instantiate(emailItemPrefab, emailListPanel);
            EmailItem emailItem = email.GetComponent<EmailItem>();
            emailItem.Setup(subjects[i], isScam[i]);
        }
    }
}