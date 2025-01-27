using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    private Animator animator;
    private Rigidbody rb;

    private Vector3 movement;

    void Start()
    {
        // Get components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Reset movement
        movement = Vector3.zero;

        // Get input for IJKL
        if (Input.GetKey(KeyCode.W)) // Forward
            movement += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) // Backward
            movement += Vector3.back;
        if (Input.GetKey(KeyCode.A)) // Left
            movement += Vector3.left;
        if (Input.GetKey(KeyCode.D)) // Right
            movement += Vector3.right;

        // Normalize movement to ensure consistent speed
        movement = movement.normalized;

        // Update animator speed parameter
        animator.SetFloat("Speed", movement.magnitude);
    }

    void FixedUpdate()
    {
        if (movement.magnitude > 0.2f)
        {
            rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }
}
