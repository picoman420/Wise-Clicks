using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI; // For LayoutRebuilder

public class JobManager : MonoBehaviour
{
    public GameObject jobItemPrefab; // JobItemTemplate prefab
    public Transform jobListPanel; // JobListContent transform
    private List<JobData> jobDataList = new List<JobData>(); // All jobs from CSV
    private Queue<JobData> jobQueue = new Queue<JobData>(); // Jobs to display
    private List<GameObject> activeJobs = new List<GameObject>(); // Current jobs in panel
    private const int maxJobsDisplayed = 5; // Max jobs shown at once

    [System.Serializable]
    private struct JobData
    {
        public string description;
        public bool isScam;
    }

    void Start()
    {
        LoadJobsFromCSV();
        InitializeJobQueue();
        SpawnInitialJobs();
    }

    void LoadJobsFromCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("Jobs");
        if (csvFile == null)
        {
            Debug.LogError("Jobs.csv not found in Resources!");
            return;
        }

        StringReader reader = new StringReader(csvFile.text);
        string header = reader.ReadLine();
        int lineNumber = 1;

        while (true)
        {
            string line = reader.ReadLine();
            if (line == null) break;
            lineNumber++;

            string[] fields = ParseCSVLine(line);
            if (fields.Length >= 2)
            {
                string isScamStr = fields[1].Trim();
                bool isScam;
                if (!bool.TryParse(isScamStr.ToLower(), out isScam))
                {
                    Debug.LogError($"Invalid boolean value '{isScamStr}' at line {lineNumber} in Jobs.csv. Expected 'true' or 'false'. Skipping entry.");
                    continue;
                }

                JobData job = new JobData
                {
                    description = fields[0].Trim(),
                    isScam = isScam
                };
                jobDataList.Add(job);
            }
            else
            {
                Debug.LogWarning($"Malformed CSV line {lineNumber}: {line}. Skipping.");
            }
        }

        if (jobDataList.Count == 0)
        {
            Debug.LogError("No valid jobs loaded from Jobs.csv!");
        }
        else
        {
            Debug.Log($"Loaded {jobDataList.Count} jobs from CSV.");
        }

        Shuffle(jobDataList);
    }

    private string[] ParseCSVLine(string line)
    {
        List<string> fields = new List<string>();
        bool inQuotes = false;
        string field = "";
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(field.Trim());
                field = "";
            }
            else
            {
                field += c;
            }
        }
        fields.Add(field.Trim());
        return fields.ToArray();
    }

    void InitializeJobQueue()
    {
        foreach (var job in jobDataList)
        {
            jobQueue.Enqueue(job);
        }
    }

    void SpawnInitialJobs()
    {
        for (int i = 0; i < maxJobsDisplayed && jobQueue.Count > 0; i++)
        {
            SpawnJob();
        }
        // Force layout rebuild after spawning all jobs
        LayoutRebuilder.ForceRebuildLayoutImmediate(jobListPanel.GetComponent<RectTransform>());
    }

    void SpawnJob()
    {
        if (jobQueue.Count == 0)
        {
            Debug.Log("No more jobs in queue!");
            return;
        }

        JobData jobData = jobQueue.Dequeue();
        GameObject job = Instantiate(jobItemPrefab, jobListPanel);
        JobItem jobItem = job.GetComponent<JobItem>();
        jobItem.Setup(jobData.description, jobData.isScam, this);
        activeJobs.Add(job);

        // Force layout rebuild after adding a new job
        LayoutRebuilder.ForceRebuildLayoutImmediate(jobListPanel.GetComponent<RectTransform>());
    }

    public void OnJobCleared(GameObject jobObject)
    {
        activeJobs.Remove(jobObject);
        if (activeJobs.Count < maxJobsDisplayed)
        {
            SpawnJob();
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}