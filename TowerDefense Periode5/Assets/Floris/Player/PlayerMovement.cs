
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Recources recources;
    Rigidbody rb;
    public PlayerInput input;
    Vector2 currentMovement;
    public float speed, maxForce;
    public Camera playerCamera;
    public Camera buildingCamera;
    bool boughtDonkey;
    public DonkeyFollowPath donkeyFollowPath;
    public InstanceVogel birdTowerScript;
    public GameObject donkey;
    public GameObject canvas;
    public GameObject pauzeMenu;
    public float xRotation;
    public float camSpeed;
    RaycastHit hit;
    public GameObject uiBuildTable;
    public GameObject uiDonkeyTower;
    public GameObject uiExplanationDonkeyTower;
    public GameObject uiExplanationBirdTower;
    public GameObject uiBird;

    public bool activatedTable;
    public bool hasBoughtBird;
    public Tutorial tutorial;
    // Start is called before the first frame update
    private void Awake()
    {
        input = new PlayerInput();
        input.Player.Movement.performed += ctx => OnMovement(ctx.ReadValue<Vector2>());
        input.Player.Movement.canceled += ctx => OnMovement(Vector2.zero);
        input.Player.PauseMenu.performed += ctx => OnPauze(ctx);

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
       
        UiHandler();
        MovementHandler();
    }
    public void MovementHandler()
    {
        
        Vector3 localMocvement = new Vector3(currentMovement.x * speed, rb.velocity.y, currentMovement.y * speed);
        Vector3 worldMovement = transform.TransformDirection(localMocvement);

        rb.velocity = new Vector3(worldMovement.x * speed * Time.deltaTime, rb.velocity.y, worldMovement.z * speed * Time.deltaTime);
    }
    public void UiHandler()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10f))
        {

            if (hit.collider.CompareTag("BuildTable"))
            {
                uiBuildTable.SetActive(true);
                if (activatedTable == true)
                {
                    uiBuildTable.SetActive(false);
                }
            }
            else if (hit.collider.tag != ("BuildTable"))
            {
                uiBuildTable.SetActive(false);
            }

            if (hit.collider.CompareTag("DonkeyHouse") && !boughtDonkey)
            {
                uiDonkeyTower.SetActive(true);
                uiExplanationDonkeyTower.SetActive(true);

            }
            else
            {

                uiExplanationDonkeyTower.SetActive(false);
                uiDonkeyTower.SetActive(false);


            }
            if (hit.collider.CompareTag("BirdTower") && !hasBoughtBird)
            {
                uiExplanationBirdTower.SetActive(true);
                uiBird.SetActive(true);
            }
            else
            {
                uiExplanationBirdTower.SetActive(false);
                uiBird.SetActive(false);

            }
        }
    }

        public void InteractPerformed(InputAction.CallbackContext context)
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10f))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag("BuildTable"))
                {
                    tutorial.tutorialStep1 = true;
                    activatedTable = true;
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


                if (hit.collider.CompareTag("DonkeyHouse") && !boughtDonkey)
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
                else if (hit.collider.CompareTag("BirdTower") && !hasBoughtBird)
                {
                    if (recources.wood >= 30 && recources.stone >= 25)
                    {
                        hasBoughtBird = true;
                        recources.wood -= 30;
                        recources.stone -= 25;
                        birdTowerScript.enabled = true;
                    }
                }

            }
        }

    

        private void OnEnable()
        {
            input.Player.Mouse.Enable();
            input.Player.Movement.Enable();
            input.Player.Interact.Enable();
            input.Player.PauseMenu.Enable();
        }
        private void OnDisable()
        {
            input.Player.PauseMenu.Disable();
            input.Player.Mouse.Disable();
            input.Player.Interact.Disable();
            input.Player.Movement.Disable();
        }
        void OnPauze(InputAction.CallbackContext context)
        {
            if (Time.timeScale != 0)
            {
                pauzeMenu.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Time.timeScale == 0)
            {
                pauzeMenu.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
            }



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

   
