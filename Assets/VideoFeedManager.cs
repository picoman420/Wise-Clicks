using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Firebase;
using Firebase.Firestore;
using System.Collections;

public class VideoFeedManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button legitimateButton;
    public Button scamButton;
    public Button nextButton;
    public RawImage videoDisplay; // Link the RawImage
    private int currentVideoIndex = 0;
    private string[] videoIds = new string[] { "video1", "video2" }; // IDs in Firestore
    private FirebaseFirestore db;

    void Start()
    {
        StartCoroutine(InitializeFirebase());
    }

    IEnumerator InitializeFirebase()
    {
        Debug.Log($"Running on platform: {Application.platform}");
        // Initialize Firebase
        var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => dependencyTask.IsCompleted);

        if (dependencyTask.Exception != null)
        {
            Debug.LogError($"Firebase initialization failed: {dependencyTask.Exception}");
            yield break;
        }

        FirebaseApp app = FirebaseApp.DefaultInstance;
        if (app.Options.DatabaseUrl == null)
        {
            Debug.LogWarning("Database URL not found in configuration. Using Firestore only.");
        }

        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Firebase initialized successfully with Firestore");

        // Wait for network connectivity
        yield return StartCoroutine(EnsureOnlineConnection());

        // Validate setup
        if (videoPlayer == null || legitimateButton == null || scamButton == null || nextButton == null || videoDisplay == null)
        {
            Debug.LogError("One or more components are not assigned!");
            yield break;
        }

        // Load initial video
        LoadVideoFromFirebase(currentVideoIndex);
    }

    IEnumerator EnsureOnlineConnection()
    {
        while (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Waiting for internet connection...");
            yield return new WaitForSeconds(1); // Wait 1 second before retrying
        }
        Debug.Log("Internet connection established.");
    }

    void LoadVideoFromFirebase(int index)
    {
        string videoId = videoIds[index];
        Debug.Log($"Attempting to load video with ID: {videoId}");

        db.Collection("videos").Document(videoId).GetSnapshotAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Failed to load video: {task.Exception}");
            }
            else
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string url = snapshot.GetValue<string>("url");
                    Debug.Log($"Loaded video URL: {url}");
                    videoPlayer.url = url;
                    videoPlayer.prepareCompleted += (source) => 
                    {
                        Debug.Log("Video prepared successfully");
                        videoDisplay.texture = videoPlayer.texture;
                        videoPlayer.Play();
                    };
                    videoPlayer.Prepare();
                }
                else
                {
                    Debug.LogError($"Document {videoId} does not exist in Firestore!");
                }
            }
        });
    }

    void OnLegitimateClicked()
    {
        Debug.Log($"Video {currentVideoIndex + 1} marked as Legitimate!");
    }

    void OnScamClicked()
    {
        Debug.Log($"Video {currentVideoIndex + 1} marked as Scam!");
    }

    void OnNextClicked()
    {
        currentVideoIndex = (currentVideoIndex + 1) % videoIds.Length;
        LoadVideoFromFirebase(currentVideoIndex);
        Debug.Log($"Switched to Video {currentVideoIndex + 1}");
    }
}