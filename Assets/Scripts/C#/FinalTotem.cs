using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalTotem : MonoBehaviour
{
    public bool activate1 = false;
    public bool activate2 = false;
    public Material S1;
    public Material S2;
    public Material S3;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = S1;
    }

    void Update()
    {
        if (activate1 && !activate2)
        {
            meshRenderer.material = S2;
        }

        else if (activate1 && activate2)
        {
            meshRenderer.material = S3;
        }

        else
        {
            meshRenderer.material = S1;
        }

        if (activate1 && activate2)
        {
            Debug.Log("WIN");
            StartCoroutine("WinCutscene"); //if any
        }

        // activate1 = false; Debug.Log("deact1" + Time.frameCount);
        // activate2 = false; Debug.Log("deact2" + Time.frameCount);
    }

    void LateUpdate()
    {
        // if (activate1 && !activate2)
        // {
        //     meshRenderer.material = S2;
        // }

        // else if (activate1 && activate2)
        // {
        //     meshRenderer.material = S3;
        // }

        // else
        // {
        //     meshRenderer.material = S1;
        // }

        // if (activate1 && activate2)
        // {
        //     Debug.Log("WIN");
        //     StartCoroutine("WinCutscene"); //if any
        // }

        activate1 = false; Debug.Log("deact1" + Time.frameCount);
        activate2 = false; Debug.Log("deact2" + Time.frameCount);
    }

    private IEnumerator WinCutscene()
    {
        yield return new WaitForSeconds(3.0f); //replace wih animations or cutscene
        SceneManager.LoadScene("Win");
    }
}
