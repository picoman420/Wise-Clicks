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
    }

    void Update()
    {
        // Update stars for all completed levels
        UpdateAllLevelStars();
    }

    void UpdateAllLevelStars()
    {
        // Check and update stars for each level
        UpdateStarsForLevel("EmailScene", emailParent, emailStarsPos);
        UpdateStarsForLevel("MessageScene", smsParent, smsStarsPos);
        UpdateStarsForLevel("CallScene", callParent, callStarsPos);
        //UpdateStarsForLevel("JobSearchScene", jobParent, jobStarsPos);
    }

    void UpdateStarsForLevel(string levelScene, Transform starsParent, Transform starsPos)
    {
        if (starsParent != null && starsPos != null)
        {
            // Clear existing stars for this level
            foreach (Transform child in starsParent)
            {
                Destroy(child.gameObject);
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
        scene = "CallScene";
        playButton.SetActive(true);
        selectLevelText.SetActive(false);
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