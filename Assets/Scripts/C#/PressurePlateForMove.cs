using UnityEngine;

public class PressurePlateForMove : MonoBehaviour
{
    public GameObject movableBarrier;
    public float moveDistance = 1f;
    public float moveSpeed = 2f;
    private Vector3 movableBarrierOriginalPosition;
    private Vector3 loweredPosition;
    private bool isPlayerOnPlate = false;

    void Start()
    {
        movableBarrierOriginalPosition = movableBarrier.transform.position; // Get original position of the barrier
        loweredPosition = movableBarrierOriginalPosition - new Vector3(0, moveDistance, 0); // Calculate lowered position
    }

    void Update()
    {
        if (movableBarrier != null)
        {
            Vector3 targetPosition = isPlayerOnPlate ? loweredPosition : movableBarrierOriginalPosition;

            // Smooth movement with a small threshold to stop jitter
            if (Vector3.Distance(movableBarrier.transform.position, targetPosition) > 0.01f)
            {
                movableBarrier.transform.position = Vector3.Lerp(
                    movableBarrier.transform.position, 
                    targetPosition, 
                    Time.deltaTime * moveSpeed
                );
            }
        }
    }

    // Detects when player or box steps on the plate
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player 1") || other.CompareTag("Player 2") || other.CompareTag("Box"))
        {
            isPlayerOnPlate = true;
        }
    }

    // Detects when player or box leaves the plate
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player 1") || other.CompareTag("Player 2") || other.CompareTag("Box"))
        {
            isPlayerOnPlate = false;
        }
    }
}
