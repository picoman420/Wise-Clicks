using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class VideoFeedManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button legitimateButton;
    public Button scamButton;
    public Button nextButton;
    public RawImage videoDisplay;
    private int currentVideoIndex = 0;
    private string[] videoUrls = new string[] { "https://wise-clicks-videos-1.s3.us-east-1.amazonaws.com/video2.mp4", "https://wise-clicks-videos-1.s3.us-east-1.amazonaws.com/video1.mp4" }; // Public S3 URLs

    void Start()
    {
        // Validate setup
        if (videoPlayer == null || legitimateButton == null || scamButton == null || nextButton == null || videoDisplay == null)
        {
            Debug.LogError("One or more components are not assigned!");
            return;
        }

        // Load initial video
        LoadVideoFromS3(currentVideoIndex);
    }

    void LoadVideoFromS3(int index)
    {
        string url = videoUrls[index];
        Debug.Log($"Attempting to load video from S3 URL: {url}");

        videoPlayer.url = url;
        videoPlayer.prepareCompleted += (source) => 
        {
            Debug.Log("Video prepared successfully");
            videoDisplay.texture = videoPlayer.texture;
            videoPlayer.Play();
        };
        videoPlayer.Prepare();
    }

    public void OnLegitimateClicked()
    {
        Debug.Log($"Video {currentVideoIndex + 1} marked as Legitimate!");
    }

    public void OnScamClicked()
    {
        Debug.Log($"Video {currentVideoIndex + 1} marked as Scam!");
    }

    public void OnNextClicked()
    {
        currentVideoIndex = (currentVideoIndex + 1) % videoUrls.Length;
        LoadVideoFromS3(currentVideoIndex);
        Debug.Log($"Switched to Video {currentVideoIndex + 1}");
    }

    public void VideoResume()
    {
        videoPlayer.Play();
    }

    public void VideoPause()
    {
        videoPlayer.Pause();
    }

    public void testClick()
    {
        Application.Quit();
    }
}