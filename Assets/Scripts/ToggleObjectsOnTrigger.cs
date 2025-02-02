using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjectsOnTrigger : MonoBehaviour
{
    [Header("GameObjects to Disable")]
    [SerializeField] private GameObject[] objectsToDisable;

    [Header("GameObjects to Enable")]
    [SerializeField] private GameObject[] objectsToEnable;

    // Called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Disable specified objects
            foreach (var obj in objectsToDisable)
            {
                if (obj != null) obj.SetActive(false);
            }

            // Enable specified objects
            foreach (var obj in objectsToEnable)
            {
                if (obj != null) obj.SetActive(true);
            }
        }
    }
}
