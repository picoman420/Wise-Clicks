using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class HomeManager : MonoBehaviour
{
    private string scene;
    public GameObject playButton;
    public GameObject selectLevelText;

    // Stars (points) system display in level map
    public Transform callParent;
    public Transform webParent;
    public Transform smsParent;
    public Transform emailParent;

    // Position reused for level selection + stars position
    public Transform callStarsPos;
    public Transform webStarsPos;
    public Transform smsStarsPos;
    public Transform emailStarsPos;

    public GameObject fullStars;
    public GameObject halfStars;
    public GameObject oneStar;
    public GameObject noStar;

    public GameObject levelSelection;
    private List<GameObject> instancesSelectionList = new List<GameObject>();

    void Start()
    {
        playButton.SetActive(false);
        selectLevelText.SetActive(true);

        // Update stars for all completed levels only if changed
        UpdateAllLevelStars();
    }

    void UpdateAllLevelStars()
    {
        // Check and update stars for each level
        UpdateStarsForLevel("EmailScene", emailParent, emailStarsPos);
        UpdateStarsForLevel("MessageScene", smsParent, smsStarsPos);
        UpdateStarsForLevel("VideoScene", webParent, webStarsPos);
        UpdateStarsForLevel("CallsScene", callParent, callStarsPos);
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
                InstantiateSelectedUI(fullStars, starsPos, starsParent, false);
            }
            else if (stars == 2)
            {
                InstantiateSelectedUI(halfStars, starsPos, starsParent, false);
            }
            else if (stars == 1)
            {
                InstantiateSelectedUI(oneStar, starsPos, starsParent, false);
            }
            else
            {
                InstantiateSelectedUI(noStar, starsPos, starsParent, false);
            }
        }
    }

    // Reused Function for instantiated UI (eg. stars & level selection)
    void InstantiateSelectedUI(GameObject instPrefab, Transform instPos, Transform instParent, bool forSelection)
    {
        // Instantiation of UI
        GameObject newInst = Instantiate(instPrefab);
        newInst.name = instPrefab.name;
        newInst.transform.SetParent(instParent, false);

        // Set anchored position of newly instantiated UI to the one passed in here
        ((RectTransform)newInst.transform).anchoredPosition = ((RectTransform)instPos).anchoredPosition; 
        
        if (forSelection)  // for level selection
        {
            levelSelection = newInst; // create instance of the instantiated
            levelSelection.GetComponent<RawImage>().enabled = true;
            levelSelection.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -125); // Set anchored position because of reusing given position 
            
            instancesSelectionList.Add(levelSelection); // Add to list to track current number of instantiated UI selection
        }
    }

    // Clear all previous selection UI
    void ClearPreviousSelection()
    {
        if (instancesSelectionList.Contains(levelSelection))
        {
            // Destroy all before instantiating a new one (to ensure only 1 appears in scene)
            foreach (GameObject child in instancesSelectionList)
            {
                Destroy(child);
            }
            instancesSelectionList.Clear();
        }
    }

    public void GoToWeb()
    {
        scene = "VideoScene";
        playButton.SetActive(true);
        selectLevelText.SetActive(false);

        // Instantiate level selection
        ClearPreviousSelection();
        InstantiateSelectedUI(levelSelection, webStarsPos, webParent, true);
    }

    public void GoToMessages()
    {
        scene = "MessageScene";
        playButton.SetActive(true);
        selectLevelText.SetActive(false);

        // Instantiate level selection
        ClearPreviousSelection();
        InstantiateSelectedUI(levelSelection, smsStarsPos, smsParent, true);
    }

    public void GoToEmail()
    {
        scene = "EmailScene";
        playButton.SetActive(true);
        selectLevelText.SetActive(false);

        // Instantiate level selection
        ClearPreviousSelection();
        InstantiateSelectedUI(levelSelection, emailStarsPos, emailParent, true);
    }

    public void GoToCalls()
    {
        scene = "CallScene";
        playButton.SetActive(true);
        selectLevelText.SetActive(false);

        // Instantiate level selection
        ClearPreviousSelection();
        InstantiateSelectedUI(levelSelection, callStarsPos, callParent, true);
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