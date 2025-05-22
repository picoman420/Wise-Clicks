using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public void GoToJobSearch()
    {
        SceneManager.LoadScene("JobSearchScene");
    }

    public void GoToMessages()
    {
        SceneManager.LoadScene("MessageScene");
    }

    public void GoToEmail()
    {
        SceneManager.LoadScene("EmailScene");
    }

    public void GoToCalls()
    {
        SceneManager.LoadScene("CallsScene"); // Placeholder for future scene
        Debug.Log("Calls scene not implemented yet!");
    }
    public void GoToHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
}