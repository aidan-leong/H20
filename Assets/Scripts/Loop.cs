using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    public Animator animator1; // Animator for the first GameObject
    public Animator animator2; // Animator for the second GameObject
    public string animation1Name; // Name of the first animation
    public string animation2Name; // Name of the second animation
    public float animation1Duration; // Duration of the first animation
    public float animation2Duration; // Duration of the second animation

    private float timer = 0f; // Timer to track animation sequence
    private bool isPlayingFirst = true; // Track which animation is playing

    void Start()
    {
        // Start by playing the first animation
        animator1.Play(animation1Name);
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        if (isPlayingFirst && timer >= animation1Duration)
        {
            // Switch to the second animation
            animator1.Play("Idle"); // Replace with an idle state or stop animation
            animator2.Play(animation2Name);
            timer = 0f;
            isPlayingFirst = false;
        }
        else if (!isPlayingFirst && timer >= animation2Duration)
        {
            // Switch back to the first animation
            animator2.Play("Idle"); // Replace with an idle state or stop animation
            animator1.Play(animation1Name);
            timer = 0f;
            isPlayingFirst = true;
        }
    }
}
