using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JobItem : MonoBehaviour
{
    public TMP_Text jobText; // Job description text
    public Button applyButton; // Apply button
    public Button markScamButton; // Mark as scam button
    private bool isScam; // Is this a scam job?

    public void Setup(string description, bool scam)
    {
        jobText.text = description;
        isScam = scam;

        applyButton.onClick.AddListener(OnApplyClicked);
        markScamButton.onClick.AddListener(OnMarkScamClicked);
    }

    void OnApplyClicked()
    {
        if (isScam)
        {
            GameManager.Instance.UpdateBalance(-100); // Deduct $100 for scam
            Debug.Log("Applied to a scam job! -$100");
        }
        else
        {
            Debug.Log("Applied to a legit job. Balance unchanged.");
        }
        Destroy(gameObject); // Remove job after interaction
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
        Destroy(gameObject); // Remove job after marking
    }
}