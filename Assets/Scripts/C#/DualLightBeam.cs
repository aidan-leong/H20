using UnityEngine;
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

    [Header("Beam Holders")]
    public List<GameObject> beamHolders;  // List of beam holders
    private List<LineRenderer> lineRenderers;

    [Header("Beam Directions")]
    public List<Vector3> beamDirections;  // List of beam directions (in local space)

    [Header("Other References")]
    public GameObject effects;
    public Sphere sphere;
    private GameManager gameManager;

    private void Start()
    {
        lineRenderers = new List<LineRenderer>();
        foreach (var holder in beamHolders)
        {
            lineRenderers.Add(holder.GetComponent<LineRenderer>());
            holder.SetActive(false);
        }

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
        if (!isActivated) return; // Prevents reactivation after Deactivate()

        bool hasAnyBeam = false;
        for (int i = 0; i < beamDirections.Count; i++)
        {
            // Transform the local beam direction to world space using the totem's rotation
            Vector3 worldDirection = transform.TransformDirection(beamDirections[i]);
            bool hasBeam = UpdateBeam(lineRenderers[i], beamHolders[i], transform.position, worldDirection);
            if (hasBeam) hasAnyBeam = true;
        }

        // If no beams are valid, deactivate the entire totem
        if (!hasAnyBeam)
        {
            Deactivate();
        }
    }

    private bool UpdateBeam(LineRenderer lineRenderer, GameObject holder, Vector3 startPoint, Vector3 direction)
    {
        List<Vector3> beamPoints = CalculateBeamPath(startPoint, direction);

        if (beamPoints.Count > 1)
        {
            if (!holder.activeSelf) Debug.Log(holder.name + " activated");
            holder.SetActive(true);
            lineRenderer.positionCount = beamPoints.Count;
            lineRenderer.SetPositions(beamPoints.ToArray());
            return true;
        }
        else
        {
            if (holder.activeSelf) Debug.Log(holder.name + " deactivated");
            holder.SetActive(false);
            return false;
        }
    }

    private List<Vector3> CalculateBeamPath(Vector3 startPoint, Vector3 direction)
    {
        List<Vector3> beamPoints = new List<Vector3> { startPoint };
        int maxReflections = 10;
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

                if (((1 << hit.collider.gameObject.layer) & mirrorLayer) != 0)
                {
                    direction = Vector3.Reflect(direction, hit.normal);
                    startPoint = hit.point;
                    reflections++;
                    continue;
                }

                break;
            }
            else
            {
                beamPoints.Add(startPoint + direction * maxBeamLength);
                break;
            }
        }
        return beamPoints;
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
        if (isActivated) return;
        isActivated = true;
        foreach (var holder in beamHolders)
        {
            holder.SetActive(true);
        }

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

        UpdateBeams();
    }

    public void Deactivate()
    {
        isActivated = false;
        foreach (var holder in beamHolders)
        {
            holder.SetActive(false);
        }

        if (sphere != null)
        {
            sphere.Activated = false;
        }

        Debug.Log("Deactivated");
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