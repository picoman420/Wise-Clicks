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


        // Use same FORMAT to instantiate chosen Stars (below r examples)

        //InstantiateStars(fullStars, callStarsPos, callParent);
        //InstantiateStars(halfStars, jobStarsPos, jobParent);
        //InstantiateStars(oneStar, smsStarsPos, smsParent);
        //InstantiateStars(noStar, emailStarsPos, emailParent);
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