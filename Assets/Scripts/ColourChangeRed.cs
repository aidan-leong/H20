using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChangeRed : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Tag of the specific trigger collider this cube should interact with.")]
    public string triggerTag = "SpecifiedTrigger";

    [Tooltip("The inactive version of the cube that will become active.")]
    public GameObject inactiveVersion;

    [Header("Audio")]
    [Tooltip("The sound to play when the tag is correct.")]
    public AudioClip correctTagSound;

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
        // Check if the trigger has the correct tag
        if (other.CompareTag(triggerTag))
        {
            // Play the sound if assigned
            if (correctTagSound != null)
            {
                audioSource.PlayOneShot(correctTagSound);

                // Delay deactivation slightly to allow the sound to play
                StartCoroutine(DeactivateAfterSound());
            }
            else
            {
                Debug.LogWarning("Correct tag sound is not assigned in the inspector!");
                DeactivateAndSwitch();
            }
        }
    }

    private IEnumerator DeactivateAfterSound()
    {
        yield return new WaitForSeconds(correctTagSound.length); // Wait for the sound to finish
        DeactivateAndSwitch();
    }

    private void DeactivateAndSwitch()
    {
        // Deactivate the current cube
        gameObject.SetActive(false);

        // Activate the inactive version
        if (inactiveVersion != null)
        {
            inactiveVersion.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Inactive version is not assigned in the inspector!");
        }
    }
}