using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f; // Adjustable bounce force
    [SerializeField] private Vector3 bounceDirection = Vector3.up; // Direction of the bounce

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a Rigidbody
        Rigidbody rb = collision.rigidbody;

        if (rb != null)
        {
            // Normalize bounce direction and apply force
            Vector3 normalizedBounceDirection = bounceDirection.normalized;
            rb.AddForce(normalizedBounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
