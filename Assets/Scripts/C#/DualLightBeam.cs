using UnityEngine;
using UnityEngine.SceneManagement;

public class DualLightBeam : MonoBehaviour
{
    [Header("Beam Settings")]
    public float maxBeamLength = 123f;
    public LayerMask hitLayers;
    public LayerMask mirrorLayer;

    [Header("Totem State")]
    public bool isActivated = false;
    public bool isCorrupted = false;
    public bool isHitByBeam = false;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // If the totem starts activated, enable the beam.
        if (isActivated)
        {
            Activate();
        }
    }

    private void Update()
    {
        if (isActivated)
        {
            // If the totem is not hit by any beam and is not the first totem, deactivate it.
            if (!isHitByBeam)
            {
                Deactivate();
            }

            // Update beams in both directions.
            UpdateBeams();
        }

        // Reset the `isHitByBeam` flag for the next frame.
        isHitByBeam = false;
    }

    public void UpdateBeams()
    {
        // Handle beam in the forward direction.
        UpdateBeam(transform.forward, 0);

        // Handle beam in the backward direction.
        UpdateBeam(-transform.forward, 2);
    }

    public void UpdateBeam(Vector3 beamDirection, int baseIndex)
    {
        Vector3 startPoint = transform.position;

        if (Physics.Raycast(startPoint, beamDirection, out RaycastHit hit, maxBeamLength, hitLayers))
        {
            // Set the beam's positions.
            SetBeamPositions(startPoint, hit.point, baseIndex);

            // Check for hit objects that implement activation logic.
            DualLightBeam dualHitTotem = hit.collider.GetComponent<DualLightBeam>();
            LightBeamTotem hitTotem = hit.collider.GetComponent<LightBeamTotem>();

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

            // Handle mirror reflection.
            if (((1 << hit.collider.gameObject.layer) & mirrorLayer) != 0)
            {
                ReflectBeam(hit, beamDirection, baseIndex);
            }

            // Handle corruption hitting the player.
            if (isCorrupted && hit.collider.CompareTag("Player"))
            {
                SceneManager.LoadScene("Lose");
            }
        }
        else
        {
            // No hit: Extend the beam to its maximum length.
            Vector3 endPoint = startPoint + beamDirection * maxBeamLength;
            SetBeamPositions(startPoint, endPoint, baseIndex);
        }
    }

    private void HandleTotemHit(object hitTotem)
    {
        if (hitTotem is DualLightBeam dualTotem)
        {
            dualTotem.isHitByBeam = true; // Mark the dual totem as hit.

            if (dualTotem.isCorrupted)
            {
                dualTotem.isCorrupted = false; // Cleanse corruption.
            }

            if (!dualTotem.isActivated)
            {
                dualTotem.Activate(); // Activate the dual totem.
            }
        }
        else if (hitTotem is LightBeamTotem lightTotem)
        {
            lightTotem.isHitByBeam = true; // Mark the light totem as hit.

            if (lightTotem.isCorrupted)
            {
                lightTotem.isCorrupted = false; // Cleanse corruption.
            }

            if (!lightTotem.isActivated)
            {
                lightTotem.Activate(); // Activate the light totem.
            }
        }
    }

    private void ReflectBeam(RaycastHit hit, Vector3 beamDirection, int baseIndex)
    {
        Vector3 reflectionDirection = Vector3.Reflect(beamDirection, hit.normal);
        Vector3 reflectedStartPoint = hit.point;

        // Cast the reflected beam to detect if it hits anything
        if (Physics.Raycast(reflectedStartPoint, reflectionDirection, out RaycastHit reflectedHit, maxBeamLength, hitLayers))
        {
            // Set the reflected beam's endpoint
            Vector3 reflectedEndPoint = reflectedHit.point;

            // Update the LineRenderer for both the main and reflected beam sections
            lineRenderer.positionCount = baseIndex + 2; // Ensure we have enough points for both parts
            lineRenderer.SetPosition(baseIndex, reflectedStartPoint);  // Start point of the reflected beam
            lineRenderer.SetPosition(baseIndex + 1, reflectedEndPoint); // End point of the reflected beam

            // Check for objects hit by the reflected beam
            DualLightBeam reflectedDualTotem = reflectedHit.collider.GetComponent<DualLightBeam>();
            LightBeamTotem reflectedTotem = reflectedHit.collider.GetComponent<LightBeamTotem>();

            // Handle the hit reflected totem (DualLightBeam or LightBeamTotem)
            if (reflectedTotem != null && reflectedTotem != this)
            {
                HandleTotemHit(reflectedTotem);
            }
            else if (reflectedDualTotem != null && reflectedDualTotem != this)
            {
                HandleTotemHit(reflectedDualTotem);
            }

            // Handle corruption hitting the player in the reflected beam
            if (isCorrupted && reflectedHit.collider.CompareTag("Player"))
            {
                SceneManager.LoadScene("Lose");
            }
        }
        else
        {
            // No hit, extend the reflected beam to its maximum length
            Vector3 reflectedEndPoint = reflectedStartPoint + reflectionDirection * maxBeamLength;

            // Update the LineRenderer for the reflected beam
            lineRenderer.positionCount = baseIndex + 2; // Ensure we have enough points for both parts
            lineRenderer.SetPosition(baseIndex, reflectedStartPoint);  // Start point of the reflected beam
            lineRenderer.SetPosition(baseIndex + 1, reflectedEndPoint); // End point of the reflected beam
        }
    }
    private void SetBeamPositions(Vector3 startPoint, Vector3 endPoint, int baseIndex)
    {
        // Ensure enough points in the LineRenderer for the specified beam section.
        lineRenderer.positionCount = Mathf.Max(lineRenderer.positionCount, baseIndex + 2);

        // Set the positions for this beam segment.
        lineRenderer.SetPosition(baseIndex, startPoint);
        lineRenderer.SetPosition(baseIndex + 1, endPoint);
    }

    public void Activate()
    {
        if (isActivated) return;

        isActivated = true;
        lineRenderer.enabled = true;
    }

    public void Deactivate()
    {
        isActivated = false;
        lineRenderer.enabled = false;
    }
}