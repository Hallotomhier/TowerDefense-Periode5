using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody rb;
    private float xrotation;

    [Header("Camera")]
    public Camera playerCamera;

    [Header("playerSettings")]
    public float speed;
    public float camSpeed;
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();
        playerInput.Player.Jump.performed += Jump_performed;
        playerInput.Player.Mouse.performed += Mouse_performed;
        playerInput.Player.Interact.performed += Interact_performed;
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 mVector2 = playerInput.Player.Movement.ReadValue<Vector2>();
        transform.Translate(mVector2.x * speed * Time.deltaTime, 0, mVector2.y * speed * Time.deltaTime);
    }

    public void Interact_performed(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
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

    

    private void Jump_performed(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }


    
}
