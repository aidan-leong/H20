using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGoneYellow : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Tag of the gameobject that should disappear upon collision.")]
    public string targetTag = "DisappearTarget";

    [Header("Audio")]
    [Tooltip("Sound to play when the target object disappears.")]
    public AudioClip disappearSound;

    private AudioSource audioSource;

    private void Start()
    {
        // Ensure the GameObject has an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has the specified tag
        if (other.CompareTag(targetTag))
        {
            // Play the disappear sound if assigned
            if (disappearSound != null)
            {
                audioSource.PlayOneShot(disappearSound);
            }

            // Deactivate the target object
            other.gameObject.SetActive(false);
        }
    }
}