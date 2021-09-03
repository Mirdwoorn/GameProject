using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls controls;
    private PlayerControls.PlayerMovementActions playerMovement;

    private Movement movement;
    private MouseLook mouseLook;

    private Vector2 horizontalInput;
    private Vector2 mouseInput;

    void Awake()
    {
        controls = new PlayerControls();
        playerMovement = controls.PlayerMovement;

        movement = GetComponent<Movement>();
        mouseLook = GetComponent<MouseLook>();

        playerMovement.HorizontalMovement.performed += callbackContext =>
        {
            horizontalInput = callbackContext.ReadValue<Vector2>();
        };

        playerMovement.MouseX.performed += callbackContext =>
        {
            mouseInput.x = callbackContext.ReadValue<float>();
        };

        playerMovement.MouseY.performed += callbackContext =>
        {
            mouseInput.y = callbackContext.ReadValue<float>();
        };
    }

    void Update()
    {
        movement.HorizontalInput = horizontalInput;

        mouseLook.MouseInput = mouseInput;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();
}