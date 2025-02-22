using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public float rotationSpeed = 30f; //degree per second
    public GameObject player;
    private bool canRotate = false;

    private void Update()
    {
        if (canRotate)
        {
            if (Input.GetKey("e") || Input.GetKey("o"))
            {
                RotateClockwise();
            }

            if (Input.GetKey("q") || Input.GetKey("u"))
            {
                RotateCounterClockwise();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canRotate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            canRotate = false;
        }
    }



    private void RotateClockwise()
    {
        player.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
    }

    private void RotateCounterClockwise()
    {
        player.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.Self);
    }
}
