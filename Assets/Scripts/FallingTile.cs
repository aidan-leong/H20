using UnityEngine;
using System.Collections;

public class FallingTile : MonoBehaviour
{
    public float fallDelay = 1.0f; // Time before the tile falls
    public float resetDelay = 2.5f; // Time before the tile resets

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Rigidbody rb;
    private bool isFalling = false;

    void Start()
    {
        // Save the original position and rotation of the tile
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // Get or add a Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.isKinematic = true; // Make the tile static by default
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFalling) // Check if the player triggered the tile
        {
            StartCoroutine(HandleTileFall());
        }
    }

    private IEnumerator HandleTileFall()
    {
        isFalling = true;

        // Wait before making the tile fall
        yield return new WaitForSeconds(fallDelay);

        rb.isKinematic = false; // Allow the tile to fall

        // Wait before resetting the tile
        yield return new WaitForSeconds(resetDelay);

        // Reset the tile to its original position and state
        rb.isKinematic = true; // Stop physics simulation
        rb.velocity = Vector3.zero; // Clear velocity
        rb.angularVelocity = Vector3.zero; // Clear angular velocity
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        isFalling = false;
    }
}
