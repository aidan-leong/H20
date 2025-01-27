using UnityEngine;
using UnityEngine.SceneManagement;

public class LightBeamTotem : MonoBehaviour
{
    [Header("Activation and Deactivation")]   
    public float maxBeamLength = 123f;
    public LayerMask hitLayers;
    private LineRenderer lineRenderer;
    public bool isActivated = false;
    public bool isCorrupted = false;

    private Collider totemCollider;
    public GameManager gameManager;

    public LayerMask mirrorLayer;
    public float reflectionMaxLength = 123f;
    public LayerMask playerLayer;

    public bool isHitByBeam = false; 
    private bool isFirstTotem = false;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        totemCollider = GetComponent<Collider>();

        if (gameObject.name == "FirstTotem" || isActivated) // Make sure the first totem is activated.
        {
            isFirstTotem = true; // Mark this as the first totem.
            Activate();
        }
    }

    private void Update()
    {
        if (isActivated)
        {
            if (!isHitByBeam && !isFirstTotem)
            {
                Deactivate();
            }

            UpdateBeam();
        }
    }

    public void UpdateBeam()
    {
        Vector3 startPoint = transform.position;
        Vector3 beamDirection = transform.forward;

        lineRenderer.positionCount = 2;

        // Change the color of the beams.
        if (isCorrupted)
        {
            lineRenderer.startColor = new Color(247f / 255f, 74f / 255f, 74f / 255f);
            lineRenderer.endColor = new Color(247f / 255f, 140f / 255f, 74f / 255f);
        }
        else
        {
            lineRenderer.startColor = new Color(227f / 255f, 227f / 255f, 84f / 255f);
            lineRenderer.endColor = Color.yellow;
        }

        if (Physics.Raycast(startPoint, beamDirection, out RaycastHit hit, maxBeamLength, hitLayers))
        {
            SetBeamPositions(startPoint, hit.point);

            // Attempt to get either LightBeamTotem or DualLightBeam
            LightBeamTotem hitTotem = hit.collider.GetComponent<LightBeamTotem>();
            DualLightBeam dualHitTotem = hit.collider.GetComponent<DualLightBeam>();

            isHitByBeam = false; // Reset the hit flag at the start of the frame.

            if (hitTotem != null)
            {
                // Handle LightBeamTotem logic
                hitTotem.isHitByBeam = true; // Mark the hit totem as being hit by this beam.

                if (hitTotem.isCorrupted)
                {
                    hitTotem.isCorrupted = false; // Cleansing of corruption.
                }

                if (!hitTotem.isActivated)
                {
                    hitTotem.Activate(); // Activate the totem
                }
            }
            else if (dualHitTotem != null)
            {
                // Handle DualLightBeam logic
                dualHitTotem.isHitByBeam = true; // Mark the dual totem as being hit by this beam.

                if (dualHitTotem.isCorrupted)
                {
                    dualHitTotem.isCorrupted = false; // Cleansing of corruption.
                }

                if (!dualHitTotem.isActivated)
                {
                    dualHitTotem.Activate(); // Activate the totem
                }
            }

            if (((1 << hit.collider.gameObject.layer) & mirrorLayer) != 0)
            {
                ReflectBeam(hit, beamDirection);
            }

            if (isCorrupted && hit.collider.CompareTag("Player")) // Player loses if hit by corruption.
            {
                SceneManager.LoadScene("Lose");
            }
        }
        else
        {
            Vector3 endPoint = startPoint + beamDirection * maxBeamLength;
            SetBeamPositions(startPoint, endPoint);
        }
    }

    public void ReflectBeam(RaycastHit hit, Vector3 beamDirection)
    {
        Vector3 reflectionDirection = Vector3.Reflect(beamDirection, hit.normal); // Reflects direction.
        Vector3 reflectedStartPoint = hit.point; // Point of where the beam hits the mirror.

        if (Physics.Raycast(reflectedStartPoint, reflectionDirection, out RaycastHit reflectedHit, reflectionMaxLength, hitLayers))
        {
            Vector3 reflectedEndPoint = reflectedHit.point;

            lineRenderer.positionCount = 3; // Increase points in line renderer to account for the reflection.
            lineRenderer.SetPosition(2, reflectedEndPoint);

            LightBeamTotem reflectedTotem = reflectedHit.collider.GetComponent<LightBeamTotem>();
            DualLightBeam reflectedDualTotem = reflectedHit.collider.GetComponent<DualLightBeam>();

            if (reflectedTotem != null && reflectedTotem != this)
            {
                reflectedTotem.isHitByBeam = true; // Mark the reflected hit totem as being hit by this beam.

                if (reflectedTotem.isCorrupted)
                {
                    reflectedTotem.isCorrupted = false;
                }

                if (!reflectedTotem.isActivated)
                {
                    reflectedTotem.Activate();
                }
            }
            else if (reflectedDualTotem != null && reflectedDualTotem != this)
            {
                reflectedDualTotem.isHitByBeam = true; // Mark the reflected hit totem as being hit by this beam.

                if (reflectedDualTotem.isCorrupted)
                {
                    reflectedDualTotem.isCorrupted = false;
                }

                if (!reflectedDualTotem.isActivated)
                {
                    reflectedDualTotem.Activate();
                }
            }

            if (isCorrupted && reflectedHit.collider.CompareTag("Player")) // Player loses if hit by corruption.
            {
                SceneManager.LoadScene("Lose");
            }
        }
        else
        {
            Vector3 reflectedEndPoint = reflectedStartPoint + reflectionDirection * reflectionMaxLength;

            lineRenderer.positionCount = 3;
            lineRenderer.SetPosition(2, reflectedEndPoint);
        }
    }

    private void SetBeamPositions(Vector3 startPoint, Vector3 endPoint) // Setting start and end points.
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }

    public void Activate() // Open door from GameManager.
    {
        isActivated = true;
        lineRenderer.enabled = true;

        if (gameObject.name.StartsWith("LastTotem_")) // Name starts with LastTotem_.
        {
            int level = int.Parse(gameObject.name.Split('_')[1].Replace("Lvl", "")); // Parse level number.
            gameManager?.ClearLevel(level);
        }

        if (gameObject.name == "LastTotem_Lvl6")
        {
            SceneManager.LoadScene("Win");
        }
    }

    public void Deactivate() // Deactivate the totem and turn off the beam.
    {
        isActivated = false;
        lineRenderer.enabled = false;
    }
}
