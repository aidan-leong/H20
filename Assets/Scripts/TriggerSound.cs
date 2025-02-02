using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TriggerSound : MonoBehaviour
{
    [Header("Audio Clip")]
    [SerializeField] private AudioClip triggerSound; // The sound to play

    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        
        // Ensure the AudioSource has the specified clip settings
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        PlaySound();
    }

    void OnCollisionEnter(Collision collision)
    {
        PlaySound();
    }

    private void PlaySound()
    {
        if (triggerSound != null && audioSource != null)
        {
            audioSource.clip = triggerSound;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No audio clip or audio source found on this object.");
        }
    }
}
