using UnityEngine;
using UnityEngine.UI; // Required for UI components
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f; // Movement speed
    private Animator animator;
    private Rigidbody rb;

    private Vector3 movement;

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

        // Ensure button is initially interactable and text is set
        if (shieldButton != null)
        {
            shieldButton.interactable = true;
            buttonText.text = "";
        }

        // Add listener to button click
        shieldButton.onClick.AddListener(OnShieldButtonPressed);
    }

    void Update()
    {
        // Reset movement
        movement = Vector3.zero;

        // Get input for WASD
        if (Input.GetKey(KeyCode.W)) // Forward
            movement += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) // Backward
            movement += Vector3.back;
        if (Input.GetKey(KeyCode.A)) // Left
            movement += Vector3.left;
        if (Input.GetKey(KeyCode.D)) // Right
            movement += Vector3.right;

        // Normalize movement to ensure consistent speed
        movement = movement.normalized;

        // Update animator speed parameter
        animator.SetFloat("Speed", movement.magnitude);

        if (Input.GetKeyDown(KeyCode.F))
        {
            OnShieldButtonPressed();
        }

        // Update cooldown timer
        if (shieldCooldownTimer > 0f)
        {
            shieldCooldownTimer -= Time.deltaTime;
            UpdateShieldButtonUI();
        }
    }

    void FixedUpdate()
    {
        if (movement.magnitude > 0.2f)
        {
            rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
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
        //Debug.Log("Shield activated");
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
            buttonText.text = $"{Mathf.Ceil(shieldCooldownTimer)}";
        }
        else
        {
            buttonText.text = "";
            shieldButton.interactable = true; // Re-enable button when cooldown ends
        }
    }
}
