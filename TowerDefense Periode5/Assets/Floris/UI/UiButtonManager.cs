using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UiButtonManager : MonoBehaviour
{
    public GameObject canvas;
    public Camera playerCam;
    public Camera buildingCam;
    public TMP_Text currentSwitch;
    public Movement movement;
    public GameObject towerBuildUI;

    int currentScene;


    public string[] towerName;

    public void Update()
    {
        currentSwitch.text = towerName[0];
    }

    public void Back()
    {
        canvas.SetActive(true);
        towerBuildUI.SetActive(false);
        playerCam.enabled = true;
        buildingCam.enabled = false;
        movement.playerInput.Enable();
        Cursor.lockState = CursorLockMode.Locked;
       
    }

    public void RestartGame()
    {
        
        if (playerCam != null)
        {
            playerCam.enabled = true;
        }

        if (buildingCam != null)
        {
            buildingCam.enabled = false;
        }

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void GoToMainMenu()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(currentScene);
    }

}
