using UnityEngine;
using UnityEngine.UI; // Required for UI components
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerMovementGuy : MonoBehaviour
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
    private PlayerInput playerInput; // Declare PlayerInput
    private InputAction cameraPanAction; // Declare InputAction
    public Transform player;
    void Awake()
    {
        // Get the PlayerInput component
        playerInput = GetComponent<PlayerInput>();

        // Get the CameraPan action from the Input Action Asset
        cameraPanAction = playerInput.actions["CameraPan"];
    }
    
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

        // Debug.Log("Vertical (Player 2): " + Input.GetAxis("Vertical (Player 2)")); // dawg whats wrong witchu
        // Debug.Log("Horizontal (P2): " + Input.GetAxis("Horizontal (P2)"));

        // Reset movement
        movement = Vector3.zero;

        // Get input for WASD
        float horizontal = Input.GetAxis("Horizontal (P2)"); // A/D or Left/Right arrow keys
        float vertical = Input.GetAxis("Vertical (Player 2)");     // W/S or Up/Down arrow keys

        // Get camera's forward and right directions (flattened to ignore vertical movement)
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate movement direction based on input and camera orientation
        movement = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // Update animator speed parameter
        animator.SetFloat("Speed", movement.magnitude);

        // Shield activation (keyboard input)
        if (Input.GetButtonDown("Shield (P2)"))
        {
            OnShieldButtonPressed();
        }

        // Sprint activation
        if (Input.GetButtonDown("Sprint (P2)") && sprintCooldownTimer <= 0f && !isSprinting)
        {
            StartCoroutine(Sprint());
        }

        // Update sprint cooldown timer
        if (sprintCooldownTimer > 0f)
        {
            sprintCooldownTimer -= Time.deltaTime;
            UpdateSprintButtonUI();
        }

        // Update shield cooldown timer
        if (shieldCooldownTimer > 0f)
        {
            shieldCooldownTimer -= Time.deltaTime;
            UpdateShieldButtonUI();
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
        // Move the player and rotate to face movement direction
        if (movement.magnitude > 0.1f)
        {
            // Move player
            float currentSpeed = isSprinting ? sprintSpeed : speed;
            rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);

            // Rotate player to face movement direction
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
        float zoomInInput = Input.GetAxis("ZoomIn");
        float zoomOutInput = Input.GetAxis("ZoomOut");
        
        if (playerCamera2 != null)
        {
            float zoomAmount = 0f;
            if (zoomInInput > 0f)
            {
                zoomAmount = -1f; // Zoom in (reduce FOV)
            }
            else if (zoomOutInput > 0f)
            {
                zoomAmount = 1f;  // Zoom out (increase FOV)
            }
            
            if (zoomAmount != 0f)
            {
                float newFieldOfView = playerCamera2.fieldOfView + (zoomAmount * zoomSpeed);
                playerCamera2.fieldOfView = Mathf.Clamp(newFieldOfView, minZoom, maxZoom);
            }
        }
    }

    private void HandleCameraPanning()
    {
        Debug.Log("RightStickHorizontal: " + Input.GetAxis("RightStickHorizontal"));

        // Read the input value from the right stick's horizontal axis
        float rotationY = cameraPanAction.ReadValue<float>() * panSpeed * Time.deltaTime;

        // Apply a deadzone to prevent drifting
        float deadzone = 0.2f; // Adjust this value as needed
        if (Mathf.Abs(rotationY) > deadzone)
        {
            // Apply rotation around the player
            playerCamera.transform.RotateAround(player.position, Vector3.up, rotationY);
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
