using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Movement : MonoBehaviour
{
    [Header("Private")]
    private PlayerInput playerInput;
    private float x;
    private float y;
    private float xrotation;
    private bool isGrounded;
    private CharacterController charCon;
    private Vector3 velocity;

    [Header("Camera")]
    public Camera playerCamera;
    public Camera buildingCamera;
    
    
    
    [Header("playerSettings")]
    public float speed;
    public float camSpeed;
    public float gravity;
    public float jumpHeight;

    [Header("UserInterface")]
    public GameObject canvas;
    RaycastHit hit;
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();
        
        playerInput.Player.Mouse.performed += Mouse_performed;
        playerInput.Player.Interact.performed += Interact_performed;
        
    }

    
    
    public void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer() 
    {
        if (isGrounded == true && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        charCon = GetComponent<CharacterController>();
        velocity.y += gravity * Time.deltaTime;
        charCon.Move(velocity * Time.deltaTime);
        
        Vector2 v2 = playerInput.Player.Movement.ReadValue<Vector2>();
        Vector3 move = transform.right * v2.x + transform.forward * v2.y;
        charCon.Move(move * speed * Time.deltaTime);

    }

    private void Mouse_performed(InputAction.CallbackContext context)
    {
        Vector2 mouserotation = context.ReadValue<Vector2>();
        float mouseX = mouserotation.x * camSpeed * Time.deltaTime;
        float mouseY = mouserotation.y * camSpeed * Time.deltaTime;

        xrotation -= mouseY;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xrotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    

    /*private void Jump_performed(InputAction.CallbackContext context)
    {
        if (isGrounded == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }*/

    public void Interact_performed(InputAction.CallbackContext context)
    {
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward , out hit, 10f))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("BuildTable"))
            {
                playerInput.Disable();
                buildingCamera.enabled = true;
                playerCamera.enabled = false;

                canvas.SetActive(true);
                if (buildingCamera)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
    }



}
