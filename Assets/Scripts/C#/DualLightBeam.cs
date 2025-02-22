using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

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
    private GameManager gameManager;
    public Health health;
    public FinalTotem finalTotem;
    public GameObject effects;
    public Sphere sphere;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Ensure references are assigned
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>(); // Assign GameManager if not set
        if (effects == null) Debug.LogWarning("Effects are not assigned in the Inspector.");
        
        if (isActivated)
        {
            Activate();
        }
    }

    private void Update()
    {
        if (sphere != null)
        {
            sphere.Activated = false; // Debugging step, can be removed later
        }
        
        if (isActivated)
        {
            if (!isHitByBeam)
            {
                Deactivate();
            }

            if (sphere != null)
            {
                sphere.Activated = true; 
            }

            UpdateBeams();
        }

        isHitByBeam = false; // Reset flag after each frame
    }

    public void UpdateBeams()
    {
        List<Vector3> beamPoints = new List<Vector3>();
        
        // Forward beam
        beamPoints.AddRange(CalculateBeamPath(transform.position, transform.forward));

        // Backward beam
        List<Vector3> backwardBeam = CalculateBeamPath(transform.position, -transform.forward);
        backwardBeam.Reverse(); // Reverse to ensure continuity.
        beamPoints.AddRange(backwardBeam);

        // Update LineRenderer
        lineRenderer.positionCount = beamPoints.Count;
        lineRenderer.SetPositions(beamPoints.ToArray());
    }

    private List<Vector3> CalculateBeamPath(Vector3 startPoint, Vector3 direction)
    {
        List<Vector3> beamPoints = new List<Vector3> { startPoint };
        int maxReflections = 3;  // Prevent infinite reflections
        int reflections = 0;

        while (reflections < maxReflections)
        {
            if (Physics.Raycast(startPoint, direction, out RaycastHit hit, maxBeamLength, hitLayers))
            {
                beamPoints.Add(hit.point);

                DualLightBeam dualHitTotem = hit.collider.GetComponent<DualLightBeam>();
                LightBeamTotem hitTotem = hit.collider.GetComponent<LightBeamTotem>();

                if (hitTotem != null)
                {
                    HandleTotemHit(hitTotem);
                }
                else if (dualHitTotem != null)
                {
                    HandleTotemHit(dualHitTotem);
                }

                // **Reflection Logic**
                if (((1 << hit.collider.gameObject.layer) & mirrorLayer) != 0)
                {
                    ReflectBeam(hit, ref direction, ref startPoint);
                    reflections++;
                    continue;  // Continue reflecting the beam
                }
                else
                {
                    break;  // Stop the loop if it's not a mirror
                }
            }
            else
            {
                beamPoints.Add(startPoint + direction * maxBeamLength);
                break;
            }
        }

        return beamPoints;
    }

    private void ReflectBeam(RaycastHit hit, ref Vector3 direction, ref Vector3 startPoint)
    {
        direction = Vector3.Reflect(direction, hit.normal);
        startPoint = hit.point + direction * 0.01f; // Move startPoint slightly forward to avoid re-hitting the same surface
    }


    private void HandleTotemHit(object hitTotem)
    {
        if (hitTotem is DualLightBeam dualTotem)
        {
            dualTotem.isHitByBeam = true;
            if (!dualTotem.isActivated) dualTotem.Activate();
        }
        else if (hitTotem is LightBeamTotem lightTotem)
        {
            lightTotem.isHitByBeam = true;
            if (!lightTotem.isActivated) lightTotem.Activate();
        }
    }

    public void Activate()
    {
        isActivated = true;
        lineRenderer.enabled = true;

        if (effects != null)
        {
            StartCoroutine(PlayEffects());
        }

        // Ensure GameManager is assigned for clearing levels
        if (gameObject.name.StartsWith("LastTotem_"))
        {
            int level = int.Parse(gameObject.name.Split('_')[1].Replace("Lvl", ""));
            gameManager?.ClearLevel(level);
        }
    }

    public void Deactivate()
    {
        isActivated = false;
        lineRenderer.enabled = false;

        if (sphere != null)
        {
            sphere.Activated = false;
        }
    }

    private IEnumerator PlayEffects()
    {
        if (effects != null)
        {
            effects.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            effects.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Effects not set. Skipping effect playback.");
        }
    }
}
