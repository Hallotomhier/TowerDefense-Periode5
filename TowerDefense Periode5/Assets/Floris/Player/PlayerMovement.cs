using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Recources recources;
    Rigidbody rb;
    public PlayerInput input;
    Vector2 currentMovement;
    public float speed;
    public Camera playerCamera;
    public Camera buildingCamera;
    bool boughtDonkey;
    public DonkeyFollowPath donkeyFollowPath;
    public GameObject donkey;
    public GameObject canvas;
    public float xRotation;
    public float camSpeed;
    RaycastHit hit;
    // Start is called before the first frame update
    private void Awake()
    {
        input = new PlayerInput();
        input.Player.Movement.performed += ctx => OnMovement(ctx.ReadValue<Vector2>());
        input.Player.Movement.canceled += ctx => OnMovement(Vector2.zero);

        input.Player.Mouse.performed += ctx => OnMouse(ctx.ReadValue<Vector2>());
        input.Player.Interact.performed += ctx => InteractPerformed(ctx);
        input.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }
    public void Update()
    {
        MovementHandler();
    }
    public void MovementHandler()
    {
        Vector3 localMocvement = new Vector3(currentMovement.x * speed, rb.velocity.y, currentMovement.y * speed);
        Vector3 worldMovement = transform.TransformDirection(localMocvement);

        rb.velocity = new Vector3(worldMovement.x * speed * Time.deltaTime, rb.velocity.y, worldMovement.z * speed * Time.deltaTime);
    }

    public void InteractPerformed(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10f))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("BuildTable"))
            {
                input.Disable();


                if (buildingCamera != null)
                {
                    buildingCamera.enabled = true;
                }

                if (playerCamera != null)
                {
                    playerCamera.enabled = false;
                }

                canvas.SetActive(true);


                if (buildingCamera != null)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
            }

            else if (hit.collider.CompareTag("DonkeyHouse") && !boughtDonkey)
            {
                if (recources.wood >= 20 && recources.stone >= 12)
                {
                    boughtDonkey = true;
                    recources.wood -= 20;
                    recources.stone -= 12;


                    if (donkeyFollowPath != null)
                    {
                        donkeyFollowPath.enabled = true;
                    }

                    if (donkey != null)
                    {
                        donkey.SetActive(true);
                    }
                }
            }
        }
    }


    private void OnEnable()
    {
        input.Player.Mouse.Enable();
        input.Player.Movement.Enable();
        input.Player.Interact.Enable();
    }
    private void OnDisable()
    {
        input.Player.Mouse.Disable();
        input.Player.Interact.Disable();
        input.Player.Movement.Disable();
    }
    void OnMouse(Vector2 delta)
    {
        float mouseX = delta.x * camSpeed * Time.deltaTime;
        float mouseY = delta.y * camSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
    void OnMovement(Vector2 value)
    {
        currentMovement = value;
    }
}
   
