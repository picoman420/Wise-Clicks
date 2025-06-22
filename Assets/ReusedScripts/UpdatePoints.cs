using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePoints : MonoBehaviour
{
    public void OnLegitOrScamClicked(bool isCorrect)
    {
        if (GameManager.Instance != null) // Null check
        {
            if (isCorrect) // True --> Correct Answer
            {
                GameManager.Instance.UpdateBalance(50);
                //Debug.Log("User chose correct answer");
            }
            else  // False --> Wrong Answer
            {
                GameManager.Instance.UpdateBalance(-100);
                //Debug.Log("User chose wrong answer");
            }
        }
    }
}