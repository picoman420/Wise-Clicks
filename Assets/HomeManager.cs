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

    private GameObject selectedHighlight; // To track the highlight UI element

    void Start()
    {
        playButton.SetActive(false);
        selectLevelText.SetActive(true);
        selectedHighlight = null; // Initialize highlight
    }

    void Update()
    {
        // Update stars for all completed levels only if changed
        UpdateAllLevelStars();
    }

    void UpdateAllLevelStars()
    {
        // Check and update stars for each level
        UpdateStarsForLevel("EmailScene", emailParent, emailStarsPos);
        UpdateStarsForLevel("MessageScene", smsParent, smsStarsPos);
        UpdateStarsForLevel("JobSearchScene", jobParent, jobStarsPos);
        UpdateStarsForLevel("CallsScene", callParent, callStarsPos);
    }

    void UpdateStarsForLevel(string levelScene, Transform starsParent, Transform starsPos)
    {
        if (starsParent != null && starsPos != null)
        {
            // Clear existing stars for this level
            foreach (Transform child in starsParent)
            {
                if (child.gameObject != selectedHighlight) // Preserve highlight if it exists
                {
                    Destroy(child.gameObject);
                }
            }

            // Get saved star rating
            int stars = GameManager.Instance != null ? GameManager.Instance.GetLevelStars(levelScene) : 0;

            // Instantiate the appropriate star prefab based on saved stars
            if (stars == 3)
            {
                InstantiateStars(fullStars, starsPos, starsParent);
            }
            else if (stars == 2)
            {
                InstantiateStars(halfStars, starsPos, starsParent);
            }
            else if (stars == 1)
            {
                InstantiateStars(oneStar, starsPos, starsParent);
            }
            else
            {
                InstantiateStars(noStar, starsPos, starsParent);
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
        UpdateSelectionHighlight(jobParent);
    }

    public void GoToMessages()
    {
        scene = "MessageScene";
        playButton.SetActive(true);
        selectLevelText.SetActive(false);
        UpdateSelectionHighlight(smsParent);
    }

    public void GoToEmail()
    {
        scene = "EmailScene";
        playButton.SetActive(true);
        selectLevelText.SetActive(false);
        UpdateSelectionHighlight(emailParent);
    }

    public void GoToCalls()
    {
        scene = "CallsScene"; // Placeholder for future scene
        playButton.SetActive(true);
        selectLevelText.SetActive(false);
        UpdateSelectionHighlight(callParent);

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

    private void UpdateSelectionHighlight(Transform levelParent)
    {
        // Clear previous highlight if it exists
        if (selectedHighlight != null)
        {
            Destroy(selectedHighlight);
        }

        // Instantiate or activate a highlight UI element (e.g., a border or image)
        GameObject highlightPrefab = Resources.Load<GameObject>("Assets/UI_Elements/Game Map/LevelSelection.png"); // Adjust path as needed
        if (highlightPrefab != null)
        {
            selectedHighlight = Instantiate(highlightPrefab, levelParent);
            ((RectTransform)selectedHighlight.transform).anchoredPosition = Vector2.zero; // Center on level icon
        }
        else
        {
            Debug.LogWarning("LevelHighlight prefab not found in Resources/UI/");
        }
    }
}