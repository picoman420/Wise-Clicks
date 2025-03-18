using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton for global access
    public TMP_Text balanceText; // Reference to BalanceText UI
    private int accountBalance = 1000; // Starting balance

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateBalanceUI();
    }

    public void UpdateBalance(int amount)
    {
        accountBalance += amount;
        UpdateBalanceUI();
        if (accountBalance <= 0)
        {
            Debug.Log("Game Over! Balance reached zero.");
            // Add game over logic here later
        }
    }

    void UpdateBalanceUI()
    {
        balanceText.text = "$" + accountBalance;
    }
}