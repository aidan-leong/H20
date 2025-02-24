using UnityEngine;

public class MoveDownOnTrigger : MonoBehaviour
{
    public GameObject targetObject; // Object that moves down
    public float moveAmount = 0.18f; // How much it moves down per trigger
    public float moveSpeed = 2f; // Speed of movement

    private Vector3 targetPosition;

    void Start()
    {
        if (targetObject != null)
        {
            targetPosition = targetObject.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetObject != null)
        {
            // Move the object down by 0.18 units
            targetPosition -= new Vector3(0, moveAmount, 0);
            StartCoroutine(MoveToTarget());
        }
    }

    private System.Collections.IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(targetObject.transform.position, targetPosition) > 0.01f)
        {
            targetObject.transform.position = Vector3.Lerp(
                targetObject.transform.position,
                targetPosition,
                Time.deltaTime * moveSpeed
            );
            yield return null;
        }

        targetObject.transform.position = targetPosition; // Snap to exact position after movement
    }
}
