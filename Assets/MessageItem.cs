using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageItem : MonoBehaviour
{
    public TMP_Text messageText; // Sender name text
    public Button readButton; // Read button
    public Button markScamButton; // Mark as scam button
    private bool isScam; // Is this a scam message?

    public void Setup(string sender, bool scam)
    {
        messageText.text = sender;
        isScam = scam;

        readButton.onClick.AddListener(OnReadClicked);
        markScamButton.onClick.AddListener(OnMarkScamClicked);
    }

    void OnReadClicked()
    {
        if (isScam)
        {
            GameManager.Instance.UpdateBalance(-150); // Deduct $150 for clicking a scam link
            Debug.Log("Clicked a scam link! -$150");
        }
        else
        {
            Debug.Log("Read a legit message. Balance unchanged.");
        }
        Destroy(gameObject); // Remove message after interaction
    }

    void OnMarkScamClicked()
    {
        if (isScam)
        {
            GameManager.Instance.UpdateBalance(50); // Reward $50 for spotting scam
            Debug.Log("Correctly marked a scam! +$50");
        }
        else
        {
            Debug.Log("That wasn’t a scam. No reward.");
        }
        Destroy(gameObject); // Remove message after marking
    }
}