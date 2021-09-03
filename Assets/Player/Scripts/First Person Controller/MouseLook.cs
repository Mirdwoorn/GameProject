using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform playerCamera;

    [SerializeField] float sensitivityX;
    [SerializeField] float sensitivityY;

    [SerializeField] float xClamp = 85.0f;
    private float xRotation;

    public Vector2 MouseInput { get; set; }

    void Update()
    {
        transform.Rotate(Vector3.up, MouseInput.x * sensitivityX * Time.deltaTime);

        xRotation -= MouseInput.y * sensitivityY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

        Vector3 playerRotation = transform.eulerAngles;
        playerRotation.x = xRotation;

        playerCamera.eulerAngles = playerRotation;
    }
}