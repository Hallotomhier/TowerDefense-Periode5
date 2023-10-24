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

    public PlayerMovement movement;
    public GameObject towerBuildUI;

    int currentScene;
    ToolTipManager toolTipManager;

    

    

    public void Back()
    {
        canvas.SetActive(true);
        towerBuildUI.SetActive(false);
        playerCam.enabled = true;
        buildingCam.enabled = false;
        movement.input.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartGame()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void GoToMainMenu()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(currentScene);
    }


}
