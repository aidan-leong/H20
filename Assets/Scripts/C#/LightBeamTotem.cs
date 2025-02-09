using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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
    public Health health;

    public int maxReflections = 5; // Number of reflections the beam can handle

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        totemCollider = GetComponent<Collider>();

        if (gameObject.name == "FirstTotem" || isActivated) 
        {
            isFirstTotem = true;
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
        List<Vector3> beamPositions = new List<Vector3>();
        Vector3 startPoint = transform.position;
        Vector3 beamDirection = transform.forward;

        beamPositions.Add(startPoint);
        int reflections = 0;

        while (reflections <= maxReflections)
        {
            if (Physics.Raycast(startPoint, beamDirection, out RaycastHit hit, maxBeamLength, hitLayers))
            {
                beamPositions.Add(hit.point);

                LightBeamTotem hitTotem = hit.collider.GetComponent<LightBeamTotem>();
                DualLightBeam dualHitTotem = hit.collider.GetComponent<DualLightBeam>();

                isHitByBeam = false;

                if (hitTotem != null)
                {
                    hitTotem.isHitByBeam = true;
                    if (hitTotem.isCorrupted) hitTotem.isCorrupted = false;
                    if (!hitTotem.isActivated) hitTotem.Activate();
                }
                else if (dualHitTotem != null)
                {
                    dualHitTotem.isHitByBeam = true;
                    if (dualHitTotem.isCorrupted) dualHitTotem.isCorrupted = false;
                    if (!dualHitTotem.isActivated) dualHitTotem.Activate();
                }

                if (((1 << hit.collider.gameObject.layer) & mirrorLayer) != 0)
                {
                    startPoint = hit.point;
                    beamDirection = Vector3.Reflect(beamDirection, hit.normal);
                    reflections++;
                    continue; // Continue reflecting
                }

                if (isCorrupted && hit.collider.CompareTag("Player"))
                {
                    health.TakeDamage(1);
                }

                break; // Stop tracing if it's not a mirror
            }
            else
            {
                beamPositions.Add(startPoint + beamDirection * maxBeamLength);
                break;
            }
        }

        UpdateLineRenderer(beamPositions);
    }

    private void UpdateLineRenderer(List<Vector3> positions)
    {
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());

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
    }

    public void Activate()
    {
        isActivated = true;
        lineRenderer.enabled = true;

        if (gameObject.name.StartsWith("LastTotem_"))
        {
            int level = int.Parse(gameObject.name.Split('_')[1].Replace("Lvl", ""));
            gameManager?.ClearLevel(level);
        }

        if (gameObject.name == "LastTotem_Lvl6")
        {
            SceneManager.LoadScene("Win");
        }
    }

    public void Deactivate()
    {
        isActivated = false;
        lineRenderer.enabled = false;
    }
}
