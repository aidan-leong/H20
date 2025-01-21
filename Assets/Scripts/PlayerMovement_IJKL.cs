using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_IJKL : MonoBehaviour
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
        if (Input.GetKey(KeyCode.I)) // Forward
            movement += Vector3.forward;
        if (Input.GetKey(KeyCode.K)) // Backward
            movement += Vector3.back;
        if (Input.GetKey(KeyCode.J)) // Left
            movement += Vector3.left;
        if (Input.GetKey(KeyCode.L)) // Right
            movement += Vector3.right;

        // Normalize movement to ensure consistent speed
        movement = movement.normalized;

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
