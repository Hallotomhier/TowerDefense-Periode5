using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiButtonManager : MonoBehaviour
{
    public GameObject canvas;
    public Camera playerCam;
    public Camera buildingCam;
    public TMP_Text currentSwitch;
    public Movement movement;

    public string[] towerName;

    public void Update()
    {
        currentSwitch.text = towerName[0];
    }

    public void Back()
    {
        canvas.SetActive(false);
        playerCam.enabled = true;
        buildingCam.enabled = false;
        movement.playerInput.Enable();
        if (!buildingCam)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


}
