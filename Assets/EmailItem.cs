using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailItem : MonoBehaviour
{
    public TMP_Text emailText; // Subject line text
    public Button openButton; // Open button
    public Button markScamButton; // Mark as scam button
    private bool isScam; // Is this a scam email?

    public void Setup(string subject, bool scam)
    {
        emailText.text = subject;
        isScam = scam;

        openButton.onClick.AddListener(OnOpenClicked);
        markScamButton.onClick.AddListener(OnMarkScamClicked);
    }

    void OnOpenClicked()
    {
        if (isScam)
        {
            GameManager.Instance.UpdateBalance(-200); // Deduct $200 for opening a scam
            Debug.Log("Opened a scam email! -$200");
        }
        else
        {
            Debug.Log("Opened a legit email. Balance unchanged.");
        }
        Destroy(gameObject); // Remove email after interaction
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
        Destroy(gameObject); // Remove email after marking
    }
}