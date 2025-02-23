using System.Collections;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public bool Activated = false;
    public Material S1;
    public Material S2;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = S1;
    }

    void Update()
    {
        //Activated = false; Debug.Log("deactivate" + Time.frameCount);
    }

    void LateUpdate()
    {
        if (Activated)
        {
            meshRenderer.material = S2;
            //Debug.Log("s2  " + Time.frameCount);
        }

        else if (!Activated)
        {
            meshRenderer.material = S1;
            //Debug.Log("s1  " + Time.frameCount);
        }
    }
}