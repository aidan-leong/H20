using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovementGuy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float sprintSpeed = 10f;
    public float sprintDuration = 3f;
    public float sprintCooldown = 5f;
    private bool isSprinting = false;
    private float sprintCooldownTimer = 0f;

    private Animator animator;
    private Rigidbody rb;
    private Vector3 movement;

    [Header("Sprint UI Settings")]
    public Button sprintButton;

    [Header("Shield Settings")]
    public GameObject shield;
    public float shieldDuration = 5.0f;
    public float shieldCD = 10.0f;
    private float shieldCooldownTimer = 0f;

    [Header("UI Elements")]
    public Button shieldButton;
    public Text buttonText;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

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

        shieldButton.onClick.AddListener(OnShieldButtonPressed);
        sprintButton.onClick.AddListener(OnSprintButtonPressed);
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");  // Left joystick X-axis
        float moveZ = Input.GetAxis("Vertical");    // Left joystick Y-axis

        movement = new Vector3(moveX, 0, moveZ);

        if (movement.magnitude > 0.1f)
        {
            movement = movement.normalized;

            // Rotate the player to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 10f);
        }

        animator.SetFloat("Speed", movement.magnitude);

        // Controller support for sprinting (Press A / Cross)
        if (Input.GetButtonDown("Jump"))  
        {
            OnSprintButtonPressed();
        }

        // Controller support for shield (Press B / Circle)
        if (Input.GetButtonDown("Fire2"))  
        {
            OnShieldButtonPressed();
        }
    }

    void FixedUpdate()
    {
        if (movement.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator Sprint()
    {
        isSprinting = true;
        animator.SetBool("IsSprinting", true);
        sprintCooldownTimer = sprintCooldown + sprintDuration;

        UpdateSprintButtonUI();

        yield return new WaitForSeconds(sprintDuration);

        isSprinting = false;
        animator.SetBool("IsSprinting", false);
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
        shieldCooldownTimer = shieldCD;
        UpdateShieldButtonUI();
        shieldButton.interactable = false;
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
