using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    [Header("Teleport Target")]
    [SerializeField] private Vector3 targetPosition; // The position to teleport the player
    [SerializeField] private Vector3 targetRotation; // The rotation to set the player to (in degrees)

    // Trigger detection
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Teleport the player
            other.transform.position = targetPosition;

            // Set the player's rotation
            other.transform.eulerAngles = targetRotation;
        }
    }
}
