using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialUi;
    public TMP_Text tutorialText;
    public bool tutorialStep1 = false;
    public bool tutorialStep2 = false;
    public bool tutorialStep3 = false;
    public bool tutorialStep4 = false;
    // Start is called before the first frame update
    void Start()
    {
        tutorialText.text = "Go to the Building table to open the building menu".ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialStep1 == true)
        {
            tutorialText.text = "Click one of the buttons for towers and click to place on land".ToString();
        }
        else if (tutorialStep2 == true)
        {
            tutorialStep1 = false;
            tutorialText.text = "Click on Raft / Rocks to place obstacles on the water Raft = a platform were you can build towers on rocks is for making a path the enemys follow".ToString();
        }
        else if (tutorialStep3 == true)
        {
            tutorialStep2 = false;
            tutorialText.text = "Upgrading towers goes the same way as placing. Press the button and click on a tower to upgrade it".ToString();
        }
        else if (tutorialStep4 == true)
        {
            tutorialStep3 = false;
            tutorialUi.SetActive(false);
        }
    }
}
