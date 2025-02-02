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

    public GameObject target;

    private Vector3 newPos;

    void Update()
    {
        camPivot = target.transform.position;
        newPos = camPivot;

        transform.eulerAngles = camRotation;
        if (GetComponent<Camera>().orthographic)
        {
            newPos += -transform.forward * camDistance * 4F;
            GetComponent<Camera>().orthographicSize = camDistance;
        }
        else
            newPos += -transform.forward * camDistance;
        newPos += transform.right * camOffset;
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * camSpeed);
    }

    // New method to move the camera to a specific position
    public void MoveCameraToDoor(Vector3 doorPosition)
    {
        // Animate the camera's movement to the specified door position
        StartCoroutine(MoveToPosition(doorPosition));
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float moveSpeed = 3.0f;
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }

        // Ensure the camera ends at the exact target position
        transform.position = targetPosition;
    }
}
