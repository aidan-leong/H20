using System.Collections;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float rollDuration = 0.5f; // Time taken to roll to the next face
    public float gridSize = 1f;      // Size of the cube and grid

    private bool isRolling = false; // Prevent simultaneous movements
	public enum CubeDirection {none, left, up, right, down};
	public CubeDirection dir = CubeDirection.none;
    public Transform cubeVisual;

    void Update()
    {
        if (!isRolling)
        {
            // Check input
            if (Input.GetKeyDown(KeyCode.W)) StartCoroutine(Roll(Vector3.forward));
            if (Input.GetKeyDown(KeyCode.S)) StartCoroutine(Roll(Vector3.back));
            if (Input.GetKeyDown(KeyCode.A)) StartCoroutine(Roll(Vector3.left));
            if (Input.GetKeyDown(KeyCode.D)) StartCoroutine(Roll(Vector3.right));
        }
    }

    private IEnumerator Roll(Vector3 direction)
    {
        isRolling = true;
        // Check for obstacles
        if (direction == Vector3.right){
            dir = CubeDirection.right;
        }
        else if (direction == Vector3.left){
            dir = CubeDirection.left;
        }
        else if (direction == Vector3.forward){
            dir = CubeDirection.up;
        }
        else {
            dir = CubeDirection.down;
        }
        if (CheckCollision(dir))
        {
            isRolling = false;
            yield break;
        }

        // Calculate rotation pivot
        Vector3 anchor = transform.position + (Vector3.down + direction) * gridSize / 2f;
        Vector3 axis = Vector3.Cross(Vector3.up, direction);

        // Perform rotation
        float elapsedTime = 0f;
        Quaternion startRotation = cubeVisual.rotation;
        Quaternion endRotation = Quaternion.AngleAxis(90f, axis) * startRotation;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + direction * gridSize;

        while (elapsedTime < rollDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / rollDuration);

            // Smoothly interpolate rotation and position
            cubeVisual.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null;
        }

        // Snap to final position and rotation to avoid precision errors
        transform.position = endPosition;
        cubeVisual.rotation = endRotation;

        isRolling = false;
    }

    private RaycastHit hit;
    private float range = 1.3f;
    bool CheckCollision(CubeDirection direction) {
        Debug.Log(direction);
        switch(direction) {
            case CubeDirection.right:
                Physics.Linecast(transform.position, transform.position + transform.right* range, out hit);
                Debug.DrawLine(transform.position, transform.position + transform.right* range, Color.black);
                break;
            case CubeDirection.left:
                Physics.Linecast(transform.position, transform.position + transform.right* -range, out hit);
                Debug.DrawLine(transform.position, transform.position + transform.right* -range, Color.black);
                break;
            case CubeDirection.up:
                Physics.Linecast(transform.position, transform.position + transform.forward* range, out hit);
                Debug.DrawLine(transform.position, transform.position + transform.forward* range, Color.black);
                break;
            case CubeDirection.down:
                Physics.Linecast(transform.position, transform.position + transform.forward* -range, out hit);
                Debug.DrawLine(transform.position, transform.position + transform.forward* -range, Color.black);
                break;
        }
    
        if ((hit.collider == null) || (hit.collider != null && hit.collider.isTrigger && !hit.collider.GetComponent("Player"))) {
            return false;
        } else {
            return true;
        }
    }
}