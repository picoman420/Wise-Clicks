using UnityEngine;
using TMPro;

public class HintManager : MonoBehaviour
{
    public TextMeshProUGUI hintText;
    public GameObject hintPanel;

    public void UpdateHint(string hint)
    {
        if (!string.IsNullOrEmpty(hint))
        {
            hintText.text = hint;
            hintPanel.SetActive(true);
        }
        else
        {
            hintPanel.SetActive(false);
        }
    }

    public void ShowHint()
    {
        hintPanel.SetActive(true);
    }
}
