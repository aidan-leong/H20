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
    public GameObject beamHolderForward;  // Holds the forward beam
    public GameObject beamHolderBackward; // Holds the backward beam

    private LineRenderer lineRendererForward;
    private LineRenderer lineRendererBackward;

    [Header("Other References")]
    public GameObject effects;
    public Sphere sphere;
    private GameManager gameManager;

    private void Start()
    {
        lineRendererForward = beamHolderForward.GetComponent<LineRenderer>();
        lineRendererBackward = beamHolderBackward.GetComponent<LineRenderer>();

        beamHolderForward.SetActive(false);
        beamHolderBackward.SetActive(false);

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

        bool hasForwardBeam = UpdateBeam(lineRendererForward, beamHolderForward, transform.position, transform.forward);
        bool hasBackwardBeam = UpdateBeam(lineRendererBackward, beamHolderBackward, transform.position, -transform.forward);

        // If neither beam is valid, deactivate the entire totem
        if (!hasForwardBeam && !hasBackwardBeam)
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
        //Debug.Log("activate  " + Time.frameCount);
        if (isActivated) return;
        isActivated = true;
        beamHolderForward.SetActive(true);
        beamHolderBackward.SetActive(true);

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
        //Debug.Log("deactivate1  " + Time.frameCount);
        isActivated = false;
        beamHolderForward.SetActive(false);
        beamHolderBackward.SetActive(false);

        if (sphere != null)
        {
            sphere.Activated = false;
        }

        Debug.Log("deactivate2  " + Time.frameCount);
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
