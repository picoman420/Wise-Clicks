using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoreManager : MonoBehaviour
{

    public Text scoreText;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE:" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (score > 50){
            SceneManager.LoadScene("Level2");
        }
        
    }
    public void AddScore()
    {
        score += 10;
        scoreText.text = "SCORE:" + score.ToString();
    }

    public void SubScore()
    {
        score -= 10;
        scoreText.text = "SCORE:" + score.ToString();
    }
}
