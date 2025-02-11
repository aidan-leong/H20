using System.Collections;
using UnityEngine;
using TMPro; // For TextMeshPro UI

public class Player1Keyboard : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float sprintDuration = 5f;
    public float sprintCooldown = 3f;
    public TextMeshProUGUI sprintStatusText;

    public Transform cameraTransform; // Camera Transform
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float panSpeed = 100f;

    private float currentSpeed;
    private bool isSprinting = false;
    private bool canSprint = true;
    private float sprintTimer = 0f;
    private float cooldownTimer = 0f;

    private Rigidbody rb;
    private float cameraDistance = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
        UpdateSprintStatus("Ready");

        if (cameraTransform != null)
        {
            cameraDistance = Vector3.Distance(cameraTransform.position, transform.position);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleSprint();
        HandleCameraControls();
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * currentSpeed + new Vector3(0, rb.velocity.y, 0); // Keep gravity effect

        if (movement != Vector3.zero)
        {
            transform.forward = movement; // Rotate to face movement direction
        }
    }

    private void HandleSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canSprint && !isSprinting)
        {
            StartSprint();
        }
        else if (isSprinting)
        {
            sprintTimer -= Time.deltaTime;

            if (sprintTimer <= 0)
            {
                StopSprint();
            }
        }
        else if (!canSprint)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
            {
                canSprint = true;
                UpdateSprintStatus("Ready");
            }
        }
    }

    private void StartSprint()
    {
        isSprinting = true;
        canSprint = false;
        sprintTimer = sprintDuration;
        cooldownTimer = sprintCooldown;
        currentSpeed = sprintSpeed;
        UpdateSprintStatus("Sprinting");
    }

    private void StopSprint()
    {
        isSprinting = false;
        currentSpeed = walkSpeed;
        UpdateSprintStatus("Cooldown");
    }

    private void UpdateSprintStatus(string status)
    {
        if (sprintStatusText != null)
        {
            sprintStatusText.text = status;
        }
    }

    private void HandleCameraControls()
    {
        if (cameraTransform == null) return;

        // Zooming
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cameraDistance -= scroll * zoomSpeed;
        cameraDistance = Mathf.Clamp(cameraDistance, minZoom, maxZoom);

        cameraTransform.position = transform.position - cameraTransform.forward * cameraDistance;

        // Panning (Right Mouse Button)
        if (Input.GetMouseButton(1))
        {
            float rotateHorizontal = Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            float rotateVertical = -Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

            cameraTransform.RotateAround(transform.position, Vector3.up, rotateHorizontal);
            cameraTransform.RotateAround(transform.position, cameraTransform.right, rotateVertical);
        }

        // Keep the camera looking at the player
        cameraTransform.LookAt(transform.position);
    }
}
