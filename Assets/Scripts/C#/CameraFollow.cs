using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 camPivot = Vector3.zero;
    public Vector3 camRotation = new Vector3(45, 35, 0);
    public float camSpeed = 5.0f;
    public float camDistance = 5.0f;
    public float camOffset = 0f;
    public float camHeightOffset = 2.0f; // Default Y-axis offset for following player

    public GameObject target;
    private Vector3 newPos;
    private bool isPanning = false;

    // Store original settings for returning to player
    private Vector3 originalRotation;
    private float originalHeightOffset;

    void Start()
    {
        // Save initial settings
        originalRotation = camRotation;
        originalHeightOffset = camHeightOffset;
    }

    void Update()
    {
        if (!isPanning)
        {
            camPivot = target.transform.position;
            camPivot.y += camHeightOffset; // Apply default Y-axis offset when following player
            newPos = CalculateCameraPosition(camPivot);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * camSpeed);
            transform.eulerAngles = camRotation;
        }
    }

    // Helper method to calculate the camera's position based on pivot, distance, and offset
    private Vector3 CalculateCameraPosition(Vector3 pivot)
    {
        Vector3 calculatedPosition = pivot;
        transform.eulerAngles = camRotation;

        if (GetComponent<Camera>().orthographic)
        {
            calculatedPosition += -transform.forward * camDistance * 4F;
            GetComponent<Camera>().orthographicSize = camDistance;
        }
        else
        {
            calculatedPosition += -transform.forward * camDistance;
        }
        calculatedPosition += transform.right * camOffset;

        return calculatedPosition;
    }

    // Move the camera to a target (e.g., door) with custom height and rotation
    public void MoveCameraToDoor(Vector3 doorPosition, float newHeightOffset, Vector3 newRotation)
    {
        // Save original settings before changing
        originalRotation = camRotation;
        originalHeightOffset = camHeightOffset;

        // Apply new height and rotation for panning
        camHeightOffset = newHeightOffset;
        camRotation = newRotation;

        // Start panning coroutine
        StartCoroutine(MoveToPosition(doorPosition));
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        isPanning = true;

        // Calculate the target camera position based on the door position
        Vector3 targetCameraPosition = CalculateCameraPosition(targetPosition + Vector3.up * camHeightOffset);

        float moveSpeed = 3.0f;
        while (Vector3.Distance(transform.position, targetCameraPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetCameraPosition, Time.deltaTime * moveSpeed);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, camRotation, Time.deltaTime * moveSpeed);
            yield return null;
        }

        // Ensure the camera ends at the exact target position
        transform.position = targetCameraPosition;
        transform.eulerAngles = camRotation;

        // Wait for a moment at the door position
        yield return new WaitForSeconds(.8f);

        // Return to following the player
        StartCoroutine(ReturnToPlayer());
    }

    private IEnumerator ReturnToPlayer()
    {
        float moveSpeed = 3.0f;

        // Restore original height and rotation
        camHeightOffset = originalHeightOffset;
        camRotation = originalRotation;

        while (Vector3.Distance(transform.position, CalculateCameraPosition(target.transform.position + Vector3.up * camHeightOffset)) > 0.1f)
        {
            Vector3 updatedTargetPos = CalculateCameraPosition(target.transform.position + Vector3.up * camHeightOffset);
            transform.position = Vector3.Lerp(transform.position, updatedTargetPos, Time.deltaTime * moveSpeed);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, camRotation, Time.deltaTime * moveSpeed);
            yield return null;
        }

        // Ensure the camera ends at the exact target position
        transform.position = CalculateCameraPosition(target.transform.position + Vector3.up * camHeightOffset);
        transform.eulerAngles = camRotation;

        // Resume normal camera following
        isPanning = false;
    }
}