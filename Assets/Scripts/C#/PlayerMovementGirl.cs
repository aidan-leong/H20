using UnityEngine;
using UnityEngine.UI; // Required for UI components
using System.Collections;

public class PlayerMovementGirl : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f; // Normal movement speed
    public float sprintSpeed = 10f; // Sprint speed
    public float sprintDuration = 3f; // Duration of sprint
    public float sprintCooldown = 5f; // Cooldown for sprint
    private bool isSprinting = false; // Is the player currently sprinting?
    private float sprintCooldownTimer = 0f; // Timer for sprint cooldown

    private Animator animator;
    private Rigidbody rb;
    private Vector3 movement;

    [Header("Sprint UI Settings")]
    public Button sprintButton; // Button to trigger sprint

    [Header("Shield Settings")]
    public GameObject shield;           // Shield object
    public float shieldDuration = 5.0f; // Duration of the shield
    public float shieldCD = 10.0f;      // Cooldown for the shield
    private float shieldCooldownTimer = 0f;

    [Header("UI Elements")]
    public Button shieldButton;  // Reference to the shield button
    public Text buttonText;      // Text on the button to display cooldown

    [Header("Camera Settings")]
    public GameObject playerCamera;          // Reference to the camera
    public Camera playerCamera2;  
    public float zoomSpeed = 2f;         // Speed of zooming
    public float minZoom = 5f;           // Minimum zoom distance
    public float maxZoom = 20f;          // Maximum zoom distance
    public float panSpeed = 10f;         // Speed of panning
    private Vector3 lastMousePosition;   // Last mouse position for panning

    public Vector3 cameraOffset = new Vector3(0, 5, -10); // Offset for camera position
    public float cameraFollowSpeed = 5f;  // Speed of camera following the player


    void Start()
    {
        // Get components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Ensure buttons are initially interactable and text is set
        if (shieldButton != null)
        {
            shieldButton.interactable = true;
            buttonText.text = "";
        }

        if (sprintButton != null)
        {
            sprintButton.interactable = true;
            SetButtonOpacity(sprintButton, 1f);
        }

        // Add listener to shield button click
        shieldButton.onClick.AddListener(OnShieldButtonPressed);
        // Add listener to sprint button click
        sprintButton.onClick.AddListener(OnSprintButtonPressed);
    }
    void Update()
    {
        if (gameObject.name != "Girl") return; // Ensures only Player 1 script moves Player 1

        movement = Vector3.zero;
        
        float moveX = 0f;
        float moveZ = 0f;

        // Keyboard input for IJKL
        if (Input.GetKey(KeyCode.A)) moveX = -1f; // Left
        if (Input.GetKey(KeyCode.D)) moveX = 1f;  // Right
        if (Input.GetKey(KeyCode.W)) moveZ = 1f;  // Up
        if (Input.GetKey(KeyCode.S)) moveZ = -1f; // Down

        // Controller input
        moveX += Input.GetAxis("Horizontal (P1)");
        moveZ += Input.GetAxis("Vertical (P1)");

        // Get camera's forward and right directions (flattened to ignore vertical movement)
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate movement direction based on input and camera orientation
        movement = (cameraForward * moveZ + cameraRight * moveX).normalized;

        // Update animator speed parameter
        animator.SetFloat("Speed", movement.magnitude);

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Fire2 (P1)")) 
        {
            OnShieldButtonPressed();
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Jump (P1)")) 
        {
            OnSprintButtonPressed();
        }

        // Handle camera zooming
        HandleCameraZoom();

        // Handle camera panning
        HandleCameraPanning();

        // Update camera position to follow the player
        HandleCameraFollow();
    }

    void FixedUpdate()
    {
        // Apply movement
        if (movement.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

            // Rotate character to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }

    private IEnumerator Sprint()
    {
        isSprinting = true;
        animator.SetBool("IsSprinting", true); // Start sprint animation
        sprintCooldownTimer = sprintCooldown + sprintDuration; // Set cooldown timer
        UpdateSprintButtonUI(); // Update UI immediately

        yield return new WaitForSeconds(sprintDuration);

        isSprinting = false;
        animator.SetBool("IsSprinting", false); // Stop sprint animation
    }

    private void OnSprintButtonPressed()
    {
        if (sprintCooldownTimer <= 0f && !isSprinting)
        {
            StartCoroutine(Sprint());
        }
    }

    private void UpdateSprintButtonUI()
    {
        if (sprintCooldownTimer > 0f)
        {
            float opacity = Mathf.Clamp01(sprintCooldownTimer / sprintCooldown);
            SetButtonOpacity(sprintButton, opacity);
            sprintButton.interactable = false;
        }
        else
        {
            SetButtonOpacity(sprintButton, 1f);
            sprintButton.interactable = true;
        }
    }

    private void SetButtonOpacity(Button button, float opacity)
    {
        Color color = button.image.color;
        color.a = opacity;
        button.image.color = color;
    }

    private void OnShieldButtonPressed()
    {
        if (shieldCooldownTimer <= 0f)
        {
            StartCoroutine(Shield());
        }
    }

    private IEnumerator Shield()
    {
        shield.SetActive(true);
        shieldCooldownTimer = shieldCD;  // Set cooldown timer after activating shield
        UpdateShieldButtonUI();         // Update UI immediately
        shieldButton.interactable = false; // Disable button during cooldown
        yield return new WaitForSeconds(shieldDuration);
        shield.SetActive(false);
    }

    private void UpdateShieldButtonUI()
    {
        if (shieldCooldownTimer > 0f)
        {
            float opacity = Mathf.Clamp01(shieldCooldownTimer / shieldCD);
            SetButtonOpacity(shieldButton, opacity);
            shieldButton.interactable = false;
        }
        else
        {
            SetButtonOpacity(shieldButton, 1f);
            shieldButton.interactable = true;
        }
    }

    private void HandleCameraZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f && playerCamera != null)
        {
            float newFieldOfView = playerCamera2.fieldOfView - scrollInput * zoomSpeed;
            playerCamera2.fieldOfView = Mathf.Clamp(newFieldOfView, minZoom, maxZoom);
        }
    }

    private void HandleCameraPanning()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

            // Convert mouse movement to camera rotation
            float rotationY = deltaMousePosition.x * panSpeed * Time.deltaTime;

            // Apply rotation around the player
            playerCamera.transform.RotateAround(transform.position, Vector3.up, rotationY);

            // Update last mouse position
            lastMousePosition = Input.mousePosition;
        }
    }

    private void HandleCameraFollow()
    {
        if (playerCamera != null)
        {
            Vector3 targetPosition = transform.position + cameraOffset;
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);
        }
    }
}
