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
        // Get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement
        movement = new Vector3(horizontal, 0, vertical).normalized;

        // Update animator speed parameter
        animator.SetFloat("Speed", movement.magnitude);
    }

    void FixedUpdate()
    {
        // Apply movement
        if (movement.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

            // Rotate character to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }
}
