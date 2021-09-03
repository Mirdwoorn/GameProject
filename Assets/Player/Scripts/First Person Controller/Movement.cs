using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] float speed;
    [SerializeField] float gravity;
    public Vector2 HorizontalInput { get; set; }

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Moving character:
        Vector3 horizontalVelocity = (transform.right * HorizontalInput.x + transform.forward * HorizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        Vector3 verticalVelocity = new Vector3(.0f, gravity, .0f);
        controller.Move(verticalVelocity * Time.deltaTime);
    }
}