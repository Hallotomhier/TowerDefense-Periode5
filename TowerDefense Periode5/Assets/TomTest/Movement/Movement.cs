using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Movement : MonoBehaviour
{
    /*
    [Header("Private")]
    public GameObject player;
    public PlayerInput playerInput;
    public GameObject Donkey;
    private float x;
    private float y;
    private float xrotation;
    private bool isGrounded;
    private CharacterController charCon;
    private Vector3 velocity;
    public bool boughtDonkey = false;
    [Header("Camera")]
    public Camera playerCamera;
    public Camera buildingCamera;

    [Header("Scripts")]
    public Recources recources;
    
    
    [Header("playerSettings")]
    public float speed;
    public float camSpeed;
    public float gravity;
    public float jumpHeight;

    [Header("UserInterface")]
   
    RaycastHit hit;
    private void Awake()
    {
        if(player != null)
        {
            playerInput = new PlayerInput();
            playerInput.Player.Enable();

            playerInput.Player.Mouse.performed += Mouse_performed;
            playerInput.Player.Interact.performed += Interact_performed;

        }

    }

    
    
    public void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(player != null)
        {
            MovePlayer();
        }
        
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

   

    public void Interact_performed(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10f))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("BuildTable"))
            {
                playerInput.Disable();


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

                    if (Donkey != null)
                    {
                        Donkey.SetActive(true);
                    }
                }
            }
        }
    }


    */
}
