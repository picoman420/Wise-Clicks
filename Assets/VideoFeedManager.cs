using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class VideoFeedManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage videoDisplay;
    public GameObject correctPickUI;
    public GameObject wrongPickUI;
    public GameObject videoButtons;
    public GameObject nextBtn;
    public GameObject mainBtn;

    // Completion Menu
    public GameObject completionMenu;
    public GameObject fullStars;
    public GameObject halfStars;
    public GameObject oneStar;
    public GameObject noStar;
    public TextMeshProUGUI score;
    public UpdatePoints updatePoints; // reference instance of the script

    // private variables
    private int currentVideoIndex = 0;
    private bool start = false;
    private string[] videoUrls = new string[] { "https://wise-clicks-videos-1.s3.us-east-1.amazonaws.com/video2.mp4", "https://wise-clicks-videos-1.s3.us-east-1.amazonaws.com/video1.mp4" }; // Public S3 URLs

    private string[] answers = {
        "Scam",
        "Real",
    };


    void Start()
    {
        // Validate setup
        if (videoPlayer == null || videoDisplay == null)
        {
            Debug.LogError("One or more components are not assigned!");
            return;
        }

        // Load initial video
        LoadVideoFromS3(currentVideoIndex);
        ManagingCompletionMenu(false);
    }

    public void LoadVideoFromS3(int index)
    {
        string url = videoUrls[index];
        Debug.Log($"Attempting to load video from S3 URL: {url}");

        videoPlayer.url = url;
        videoPlayer.prepareCompleted += (source) => 
        {
            Debug.Log("Video prepared successfully");
            videoDisplay.texture = videoPlayer.texture;
        };
        videoPlayer.Prepare();

        if (start == true)
        {
            VideoResume();  // set to default
        }
        else
        {
            VideoPause();
            start = true;
        }
    }

    // Check for completion of game and Set Visibility of number of stars to display
    public void CompletionGame()
    {
        completionMenu.SetActive(true);

        // Save the score before displaying
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveScore();
        }

        // Get current balance as score
        int currentScore = GameManager.Instance != null ? GameManager.Instance.GetAccountBalance() : 1000;

        // Update score text
        if (score != null)
        {
            score.text = "SCORE: " + currentScore.ToString();
        }
        else
        {
            Debug.LogWarning("score TextMeshProUGUI is not assigned!");
        }

        // Calculate and display stars based on score
        CalculateStars(currentScore);
    }

    private void CalculateStars(int score)
    {
        // Reset all star displays
        fullStars.SetActive(false);
        halfStars.SetActive(false);
        oneStar.SetActive(false);
        noStar.SetActive(false);

        // Determine star rating based on score (3-star system)
        if (score >= 1300) // 3 stars (87% of max 1500)
        {
            fullStars.SetActive(true); // 3 stars
        }
        else if (score >= 1000) // 2 stars (67% of max)
        {
            halfStars.SetActive(true); // 2 stars
        }
        else if (score >= 500) // 1 star (33% of max)
        {
            oneStar.SetActive(true); // 1 star
        }
        else // 0 stars (<33% of max)
        {
            noStar.SetActive(true); // 0 stars
        }
    }

    public void CompleteExitToLevelMap()
    {
        if (GameManager.Instance != null)
        {
            int currentScore = GameManager.Instance.GetAccountBalance();
            int stars = 0;

            if (currentScore >= 1300)
            {
                stars = 3;
            }
            else if (currentScore >= 1000)
            {
                stars = 2;
            }
            else if (currentScore >= 500)
            {
                stars = 1;
            }
            else
            {
                stars = 0;
            }

            GameManager.Instance.SaveLevelStars(SceneManager.GetActiveScene().name, stars); // Save stars for this level
        }

        SceneManager.LoadScene("LevelScene");
    }


    void Reset(bool pauseV, bool contV)
    {
        GameObject pauseBtn = videoButtons.transform.GetChild(0).gameObject;
        GameObject continueBtn = videoButtons.transform.GetChild(1).gameObject;

        pauseBtn.SetActive(pauseV);
        continueBtn.SetActive(contV);
        videoButtons.SetActive(true);
    }

    public void Replay()
    {
        // Reset balance to 1000
        if (GameManager.Instance != null)
        {
            int currentBalance = GameManager.Instance.GetAccountBalance();
            GameManager.Instance.UpdateBalance(1000 - currentBalance);
        }

        currentVideoIndex = 0;
        LoadVideoFromS3(0);

        Reset(true, false);
        ManagingCompletionMenu(false);
        correctPickUI.SetActive(false);
        wrongPickUI.SetActive(false);
        nextBtn.SetActive(false);
        mainBtn.SetActive(true);
    }


    public void OnNextClicked()
    {
        if (currentVideoIndex+1 == videoUrls.Length)
        {
            videoPlayer.Stop();
            CompletionGame();
        }
        else
        {
            currentVideoIndex = (currentVideoIndex + 1) % videoUrls.Length;
            LoadVideoFromS3(currentVideoIndex);

            correctPickUI.SetActive(false);
            wrongPickUI.SetActive(false);

            Debug.Log($"Switched to Video {currentVideoIndex + 1}");
        }

    }

    // Check user answers, given that the videos sequence are fixed 
    public void AnsClicked(string userAns)
    {
        videoPlayer.Stop();
        videoButtons.SetActive(false);

        if (string.Equals(userAns, answers[currentVideoIndex]))
        {
            correctPickUI.SetActive(true);

            if (updatePoints != null)
            {
                updatePoints.OnLegitOrScamClicked(true);
            }
        }
        else
        {
            wrongPickUI.SetActive(true);

            if (updatePoints != null)
            {
                updatePoints.OnLegitOrScamClicked(false);
            }
        }
    }

    public void VideoResume()
    {
        Reset(true, false);  // set to default
        videoPlayer.Play();
    }

    public void VideoPause()
    {
        Reset(false, true);
        videoPlayer.Pause();
    }
    
    public void QuitLevel()
    {
        SceneManager.LoadScene("LevelScene");
    }
    private void ManagingCompletionMenu(bool show)
    {
        // Set Active False to stars
        fullStars.SetActive(show);
        halfStars.SetActive(show);
        oneStar.SetActive(show);
        noStar.SetActive(show);

        completionMenu.SetActive(show);
    }
}