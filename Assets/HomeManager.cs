using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    private string scene;
    public GameObject playButton;
    public GameObject selectLevelText;

    // Stars (points) system display in level map
    public Transform callParent;
    public Transform jobParent;
    public Transform smsParent;
    public Transform emailParent;

    public Transform callStarsPos;
    public Transform jobStarsPos;
    public Transform smsStarsPos;
    public Transform emailStarsPos;

    public GameObject fullStars;
    public GameObject halfStars;
    public GameObject oneStar;
    public GameObject noStar;

    void Start()
    {
        playButton.SetActive(false);
        selectLevelText.SetActive(true);

        // Update stars for completed levels
        UpdateLevelStars();
    }

    void UpdateLevelStars()
    {
        int currentScore = GameManager.Instance != null ? GameManager.Instance.GetAccountBalance() : 1000;
        string lastScene = PlayerPrefs.GetString("LastCompletedLevel", ""); // Track last completed level

        // Determine which level to update stars for based on last completed scene
        if (!string.IsNullOrEmpty(lastScene))
        {
            Transform starsParent = null;
            Transform starsPos = null;

            switch (lastScene)
            {
                case "EmailScene":
                    starsParent = emailParent;
                    starsPos = emailStarsPos;
                    break;
                case "MessageScene":
                    starsParent = smsParent;
                    starsPos = smsStarsPos;
                    break;
                case "JobSearchScene":
                    starsParent = jobParent;
                    starsPos = jobStarsPos;
                    break;
                case "CallsScene":
                    starsParent = callParent;
                    starsPos = callStarsPos;
                    break;
            }

            if (starsParent != null && starsPos != null)
            {
                // Clear existing stars for this level
                foreach (Transform child in starsParent)
                {
                    Destroy(child.gameObject);
                }

                // Instantiate the appropriate star prefab based on score
                if (currentScore >= 1300) // 3 stars
                {
                    InstantiateStars(fullStars, starsPos, starsParent);
                }
                else if (currentScore >= 1000) // 2 stars
                {
                    InstantiateStars(halfStars, starsPos, starsParent);
                }
                else if (currentScore >= 500) // 1 star
                {
                    InstantiateStars(oneStar, starsPos, starsParent);
                }
                else // 0 stars
                {
                    InstantiateStars(noStar, starsPos, starsParent);
                }
            }
        }
    }

    void InstantiateStars(GameObject starsPrefab, Transform starsPos, Transform starsParent)
    {
        GameObject newStars = Instantiate(starsPrefab);
        newStars.transform.SetParent(starsParent, false);

        ((RectTransform)newStars.transform).anchoredPosition = ((RectTransform)starsPos).anchoredPosition;
    }

    public void GoToJobSearch()
    {
        scene = "JobSearchScene";
        playButton.SetActive(true);
        selectLevelText.SetActive(false);
    }

    public void GoToMessages()
    {
        scene = "MessageScene";
        playButton.SetActive(true);
        selectLevelText.SetActive(false);
    }

    public void GoToEmail()
    {
        scene = "EmailScene";
        playButton.SetActive(true);
        selectLevelText.SetActive(false);
    }

    public void GoToCalls()
    {
        scene = "CallsScene"; // Placeholder for future scene
        playButton.SetActive(true);
        selectLevelText.SetActive(false);

        Debug.Log("Calls scene not implemented yet!");
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void LoadScene()
    {
        if (string.IsNullOrWhiteSpace(scene))
        {
            Debug.Log("Please select a level!");
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }
}