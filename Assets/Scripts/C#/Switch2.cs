using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch2 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Level1GameManager.trigger2 = true;
    }

    
    void OnTriggerExit(Collider other)
    {
        Level1GameManager.trigger2 = false;
    }
}
