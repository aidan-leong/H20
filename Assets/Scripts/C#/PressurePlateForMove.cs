using UnityEngine;

public class PressurePlateForMove : MonoBehaviour
{
    public GameObject movableBarrier;
    public float moveDistance = 1f;
    public float moveSpeed = 2f;
    private Vector3 movableBarrierOriginalPosition;
    private bool isPlayerOnPlate = false;

    void Start()
    {
        movableBarrierOriginalPosition = movableBarrier.transform.position; //get og position of barrier
    }

    void Update()
    {
        if(movableBarrier != null)
        {
            Vector3 targetPosition = isPlayerOnPlate
                ? movableBarrierOriginalPosition - new Vector3(0, moveDistance, 0) //tenary condition, if true move down
                : movableBarrierOriginalPosition; //if false move back to og position

            //move in the barrier 
         movableBarrier.transform.position = Vector3.Lerp(movableBarrier.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
    }

    //checks for collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player 1") || other.CompareTag("Player 2") || other.CompareTag("Box"))
        {
            isPlayerOnPlate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player 2") || other.CompareTag("Box"))
        {
            isPlayerOnPlate = false;
        }
    }
}
