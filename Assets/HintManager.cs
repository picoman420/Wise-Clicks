using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    public GameObject[] messagePanels; // Assign Msg1Panel, Msg2Panel, etc.
    public string[] hints; // Assign hints for each panel
    public Text hintText; // Assign the Text in HintPanel
    public GameObject hintPanel; // Assign the HintPanel

    public void ShowHint()
    {
        for (int i = 0; i < messagePanels.Length; i++)
        {
            if (messagePanels[i].activeSelf) // Check which panel is active
            {
                hintText.text = hints[i];
                hintPanel.SetActive(true); // Show hint panel
                return;
            }
        }
        hintPanel.SetActive(false); // Hide hint if no panel is active
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
