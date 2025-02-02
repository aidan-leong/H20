using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourTriggerSwitch1 : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Player Cube")
        {
            transform.parent.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 1);
        }
    }

    private IEnumerator OnTriggerExit(Collider other)
    {
        yield return new WaitForSeconds(1f);
        transform.parent.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 1, 1);
    }
}
