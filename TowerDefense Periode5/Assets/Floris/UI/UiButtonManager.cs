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
    public GameObject pauzeMenuGame;
    public GameObject settingsMenu;
    int currentScene;
   
    public SpawnManager spawnManager;

    

    public void PauzeMenuGameBack()
    {
        pauzeMenuGame.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SettingInGame()
    {
         pauzeMenuGame.SetActive(false);
         settingsMenu.SetActive(true);
       
    }
    public void BackSettingsMenuinGame()
    {
        settingsMenu.SetActive(false);
        pauzeMenuGame.SetActive(true);
    }
   
    public void Back()
    {
        canvas.SetActive(true);
        towerBuildUI.SetActive(false);
        playerCam.enabled = true;
        buildingCam.enabled = false;
        movement.input.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        movement.activatedTable = false;
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

    public void StartNextWave()
    {
        spawnManager.timer = 20;
        spawnManager.gameState = SpawnManager.GameState.BuildPhase;
    }

}
