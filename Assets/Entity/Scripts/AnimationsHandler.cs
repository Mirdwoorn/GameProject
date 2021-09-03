using UnityEngine;

public class AnimationsHandler : MonoBehaviour
{
    private MouseLook mouseLook;
    private Movement movement;

    [SerializeField] Animator animator;

    private EntityState state;

    void Awake()
    {
        mouseLook = GetComponent<MouseLook>();
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (movement.HorizontalInput == Vector2.zero)
            state = EntityState.Idle;
        else
            state = EntityState.Walking;
        animator.SetInteger("State", (int)state);
    }
}