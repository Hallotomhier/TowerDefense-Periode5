using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class UiButtonManager : MonoBehaviour
{
    public GameObject canvas;
    public Camera playerCam;
    public Camera buildingCam;
    public GameObject player;
    public PlayerMovement movement;
    public GameObject towerBuildUI;
    public GameObject pauzeMenuGame;
    public GameObject settingsMenu;
    public Recources recources;
    public GameObject victoryUI;
    public GameObject loseUI;
    public GameObject devTool;

    public SpawnManager spawnManager;
    public Slider zoomSlider;
    public Slider brightSlider;
    public float maxBrightness;
    public float minBrightness; 
    public float maxZoom;
    public int minZoom;
    public Transform positionStart;

    public void Zoom()
    {
        float sliderValue = zoomSlider.value;
        float newZoom = Mathf.Lerp(maxZoom, minZoom, sliderValue);

        Vector3 camPos = buildingCam.transform.position;
        camPos.y = newZoom;
        buildingCam.transform.position = camPos;

    }
   
    public void ResetPosition()
    {
        player.transform.position = positionStart.position;
    }

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

        SceneManager.LoadScene("Main Menu");
    }

    public void StartNextWave()
    {
        spawnManager.timer = 40;
        spawnManager.gameState = SpawnManager.GameState.BuildPhase;
    }
    public void CheatRecources()
    {
        recources.wood += 9000;
        recources.stone += 9000;


    }
    public void SpeedItUP()
    {
        Time.timeScale = 1;
    }
    public void SpeedUp2x()
    {
        Time.timeScale = 2;
    }
    public void WinGame()
    {
        victoryUI.SetActive(true);
        devTool.SetActive(false);
        Time.timeScale = 0;
    }
    public void LoseGame()
    {
        loseUI.SetActive(true);
        devTool.SetActive(false);
        Time.timeScale = 0;
    }

    
}