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

        if (isActivated)
        {
            Activate();
        }
    }

    private void Update()
    {
        if (sphere != null)
        {
            sphere.Activated = false; //Debug.Log("deactivate" + Time.frameCount);
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

        isHitByBeam = false;
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
        int maxReflections = 10;
        int reflections = 0;
        
        while (reflections < maxReflections)
        {
            if (Physics.Raycast(startPoint, direction, out RaycastHit hit, maxBeamLength, hitLayers))
            {
                beamPoints.Add(hit.point);
                
                DualLightBeam dualHitTotem = hit.collider.GetComponent<DualLightBeam>();
                LightBeamTotem hitTotem = hit.collider.GetComponent<LightBeamTotem>();
                FinalTotem finalHitTotem = hit.collider.GetComponent<FinalTotem>();
                
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
                    ReflectBeam(hit, ref direction, ref startPoint);
                    reflections++;
                    continue;
                }

                else if (finalHitTotem != null)
                {
                    switch (finalTotem.activate1)
                    {
                        case false: finalTotem.activate1 = true; break;
                        case true: finalTotem.activate2 = true; break;
                    }
                }
                
                if (isCorrupted && hit.collider.CompareTag("Player"))
                {
                    health.TakeDamage(1);
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

    private void ReflectBeam(RaycastHit hit, ref Vector3 direction, ref Vector3 startPoint)
    {
        direction = Vector3.Reflect(direction, hit.normal);
        startPoint = hit.point;
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
        effects.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        effects.SetActive(false);
    }
}