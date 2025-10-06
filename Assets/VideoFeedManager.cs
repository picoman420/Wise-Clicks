using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.SceneManagement;

public class VideoFeedManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage videoDisplay;
    public GameObject correctPickUI;
    public GameObject wrongPickUI;
    public GameObject videoButtons;

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
        Reset(true, false);

        LoadVideoFromS3(0);
        currentVideoIndex = 0;
    }


    public void OnNextClicked()
    {
        currentVideoIndex = (currentVideoIndex + 1) % videoUrls.Length;
        LoadVideoFromS3(currentVideoIndex);

        correctPickUI.SetActive(false);
        wrongPickUI.SetActive(false);

        Debug.Log($"Switched to Video {currentVideoIndex + 1}");
    }

    // Check user answers, given that the videos sequence are fixed 
    public void AnsClicked(string userAns)
    {
        videoPlayer.Stop();
        videoButtons.SetActive(false);

        if (string.Equals(userAns, answers[currentVideoIndex]))
        {
            correctPickUI.SetActive(true);
        }
        else
        {
            wrongPickUI.SetActive(true);
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
}