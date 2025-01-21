using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowK : MonoBehaviour
{
    public Vector3 camPivot = Vector3.zero;         // The pivot point for the camera (target position)
    public Vector3 camRotation = new Vector3(31, 0, 0); // The rotation of the camera

    public float camSpeed = 14.0f;                  // The speed at which the camera follows
    public float camDistance = 1.88f;              // The distance from the target
    public float camOffset = 0f;                  // Horizontal offset from the target
    public float camHeight = 1.15f;                // Vertical offset from the target

    public GameObject target;                     // The target GameObject the camera will follow

    private Vector3 newPos;                       // The new calculated position of the camera

    void Update()
    {
        // Update the pivot point to the target's position
        camPivot = target.transform.position;

        // Start with the target's position
        newPos = camPivot;

        // Apply the camera rotation
        transform.eulerAngles = camRotation;

        // Calculate the new camera position
        if (GetComponent<Camera>().orthographic)
        {
            newPos += -transform.forward * camDistance * 4f; // Adjust for orthographic projection
            GetComponent<Camera>().orthographicSize = camDistance;
        }
        else
        {
            newPos += -transform.forward * camDistance; // Adjust for perspective projection
        }

        // Apply height and horizontal offset
        newPos += Vector3.up * camHeight;        // Add height to the camera position
        newPos += transform.right * camOffset;  // Add horizontal offset to the camera position

        // Smoothly interpolate the camera's position
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * camSpeed);
    }
}
