using UnityEngine;

public class JobManager : MonoBehaviour
{
    public GameObject jobItemPrefab; // JobItemTemplate prefab
    public Transform jobListPanel; // JobListPanel transform

    void Start()
    {
        SpawnJobs();
    }

    void SpawnJobs()
    {
        // Example jobs (expand this list later)
        string[] jobDescriptions = {
            "Work-from-home, $5000/week!", // Scam
            "Retail Assistant, $15/hour",   // Legit
            "Pay $200 for training, earn big!", // Scam
            "Part-time cashier, no fees"    // Legit
        };
        bool[] isScam = { true, false, true, false };

        for (int i = 0; i < jobDescriptions.Length; i++)
        {
            GameObject job = Instantiate(jobItemPrefab, jobListPanel);
            JobItem jobItem = job.GetComponent<JobItem>();
            jobItem.Setup(jobDescriptions[i], isScam[i]);
        }
    }
}