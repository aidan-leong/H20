using UnityEngine;
using UnityEngine.InputSystem;

public class RotatingObject : MonoBehaviour
{
    public float rotationSpeed = 30f; // Degrees per second

    // Target GameObjects for each player
    public GameObject targetObjectForGirl; // Object to rotate when Girl is in the collider
    public GameObject targetObjectForGuy;  // Object to rotate when Guy is in the collider

    // Input Action for D-pad
    private InputAction dpadLeftAction;
    private InputAction dpadRightAction;

    // Track which player is in this object's collider
    private GameObject playerInCollider;

    private void Awake()
    {
        dpadLeftAction = new InputAction("DpadLeft", binding: "<Gamepad>/dpad/left");
        dpadRightAction = new InputAction("DpadRight", binding: "<Gamepad>/dpad/right");

        // Enable the actions
        dpadLeftAction.Enable();
        dpadRightAction.Enable();
    }

    private void OnEnable()
    {
        dpadLeftAction.Enable();
        dpadRightAction.Enable();
    }

    private void OnDisable()
    {
        dpadLeftAction.Disable();
        dpadRightAction.Disable();
    }

    private void Update()
    {
        if (playerInCollider != null)
        {
            // Check if the player in the collider is "Girl" (keyboard controls)
            if (playerInCollider.CompareTag("Player 1"))
            {
                // Keyboard input for Girl
                if (Input.GetKey("e") || Input.GetKey("o"))
                {
                    RotateClockwise(targetObjectForGirl);
                }
                if (Input.GetKey("q") || Input.GetKey("u"))
                {
                    RotateCounterClockwise(targetObjectForGirl);
                }
            }
            // Check if the player in the collider is "Guy" (D-pad controls)
            if (playerInCollider != null && playerInCollider.CompareTag("Player 2"))
            {
                // Check D-pad left input
                if (dpadLeftAction.ReadValue<float>() > 0)
                {
                    RotateCounterClockwise(targetObjectForGuy);
                }
                // Check D-pad right input
                if (dpadRightAction.ReadValue<float>() > 0)
                {
                    RotateClockwise(targetObjectForGuy);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is a player
        if (other.CompareTag("Player 1") || other.CompareTag("Player 2"))
        {
            playerInCollider = other.gameObject; // Track which player is in the collider
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is the tracked player
        if (other.gameObject == playerInCollider)
        {
            playerInCollider = null; // Clear the tracked player
        }
    }

    private void RotateClockwise(GameObject targetObject)
    {
        if (targetObject != null)
        {
            targetObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
        }
    }

    private void RotateCounterClockwise(GameObject targetObject)
    {
        if (targetObject != null)
        {
            targetObject.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.Self);
        }
    }
}