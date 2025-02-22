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
        // Reset movement
        movement = Vector3.zero;

        // Get input for IJKL
        if (Input.GetKey(KeyCode.W)) // Forward
            movement += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) // Backward
            movement += Vector3.back;
        if (Input.GetKey(KeyCode.A)) // Left
            movement += Vector3.left;
        if (Input.GetKey(KeyCode.D)) // Right
            movement += Vector3.right;

        if (Input.GetKey(KeyCode.F)){
            OnShieldButtonPressed();
        }

        // Normalize movement to ensure consistent speed
        movement = movement.normalized;

        // Update animator speed parameter
        animator.SetFloat("Speed", movement.magnitude);
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
}
