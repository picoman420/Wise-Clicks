using UnityEngine;
using TMPro;

public class HintController : MonoBehaviour
{
    public GameObject hintText;
    public float hintDuration = 5f;
    private float cooldown = 0f;

    void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;
    }

    public void ShowHint()
    {
        if (cooldown <= 0)
        {
            hintText.SetActive(true);
            Invoke("HideHint", hintDuration);
            cooldown = hintDuration + 2f; // Cooldown after hint
        }
    }

    void HideHint()
    {
        hintText.SetActive(false);
    }
}