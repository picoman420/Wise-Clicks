using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text balanceText; // Reference to BalanceText UI
    private int accountBalance = 1000;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateBalanceUI();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignBalanceText();
        UpdateBalanceUI();
    }

    // New method to assign balanceText
    void AssignBalanceText()
    {
        GameObject balanceTextObj = GameObject.Find("BalanceText");
        if (balanceTextObj != null)
        {
            balanceText = balanceTextObj.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogWarning("No BalanceText found in scene: " + SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateBalance(int amount)
    {
        accountBalance += amount;
        UpdateBalanceUI();
        if (accountBalance <= 0)
        {
            Debug.Log("Game Over! Balance reached zero.");
        }
    }

    void UpdateBalanceUI()
    {
        // If balanceText is null, try to find it
        if (balanceText == null)
        {
            AssignBalanceText();
        }

        // Only update if balanceText is valid
        if (balanceText != null)
        {
            balanceText.text = "$" + accountBalance;
        }
        else
        {
            Debug.LogWarning("Cannot update balance UI: balanceText is still null.");
        }
    }
}