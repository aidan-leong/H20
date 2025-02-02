using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTime : MonoBehaviour
{
    public float startTime = 120.0f; // Time given to complete the game
    private float timeRemaining;

    [Header("Game Objects to Activate")]
    [SerializeField] private GameObject[] objectsToActivate; // Elements to activate when time runs out

    [Header("Game Objects to Deactivate")]
    [SerializeField] private GameObject[] objectsToDeactivate; // Elements to deactivate when time runs out

    private bool isTimeUp = false; // Ensure time-up logic runs only once

    void Start()
    {
        GetComponent<Text>().material.color = Color.white; // GUI text color
        timeRemaining = startTime;
    }

    void Update()
    {
        if (!isTimeUp) // Only update if time hasn't run out
        {
            CountDown();
        }
    }

    void CountDown()
    {
        timeRemaining = startTime - Time.timeSinceLevelLoad;
        ShowTime();

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            TimeIsUp();
        }
    }

    void ShowTime()
    {
        int minutes = (int)timeRemaining / 60; // Derive minutes
        int seconds = (int)timeRemaining % 60; // Derive seconds
        string timeString = "TIMER " + minutes.ToString() + ":" + seconds.ToString("d2");
        GetComponent<Text>().text = timeString;
    }

    void TimeIsUp()
    {
        isTimeUp = true; // Prevent multiple activations
        Time.timeScale = 0; // Pause the game

        // Activate specified GameObjects
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        // Deactivate specified GameObjects
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
