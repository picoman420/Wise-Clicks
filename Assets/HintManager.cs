using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintManager : MonoBehaviour
{
    public GameObject[] messagePanels;
    public TextMeshProUGUI hintText; // Assign Msg1Panel, Msg2Panel, etc.
    public string[] hints; // Assign hints for each panel

    public GameObject hintPanel; // Assign the HintPanel

    public void ShowHint()
    {   
        bool hintFound = false;
        for (int i = 0; i < messagePanels.Length; i++)
        {
            if (messagePanels[i].activeSelf) // Check which panel is active
            {
                hintText.text = hints[i]; // Update hint text
                hintFound = true;
                break;
            }
        }
        hintPanel.SetActive(hintFound); // Hide hint if no panel is active
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
