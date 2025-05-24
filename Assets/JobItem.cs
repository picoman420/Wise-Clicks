using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JobItem : MonoBehaviour
{
    public TMP_Text jobText; // Job description text
    public Button applyButton; // Apply button
    public Button markScamButton; // Mark as scam button
    private bool isScam; // Is this a scam job?
    private JobManager jobManager; // Reference to JobManager

    public void Setup(string description, bool scam, JobManager manager)
    {
        jobText.text = description;
        isScam = scam;
        jobManager = manager;

        applyButton.onClick.AddListener(OnApplyClicked);
        markScamButton.onClick.AddListener(OnMarkScamClicked);

        // Force layout rebuild
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    void OnApplyClicked()
    {
        bool isCorrectDecision = !isScam; // Correct if applying to a legit job
        if (isScam)
        {
            GameManager.Instance.UpdateBalance(-100);
            Debug.Log("Applied to a scam job! -$100");
        }
        else
        {
            Debug.Log("Applied to a legit job. Balance unchanged.");
        }
        jobManager.OnJobCleared(gameObject, isCorrectDecision);
        Destroy(gameObject);
    }

    void OnMarkScamClicked()
    {
        bool isCorrectDecision = isScam; // Correct if marking a scam job
        if (isScam)
        {
            GameManager.Instance.UpdateBalance(50);
            Debug.Log("Correctly marked a scam! +$50");
        }
        else
        {
            Debug.Log("That wasn’t a scam. No reward.");
        }
        jobManager.OnJobCleared(gameObject, isCorrectDecision);
        Destroy(gameObject);
    }
}