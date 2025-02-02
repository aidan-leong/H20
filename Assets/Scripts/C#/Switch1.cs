using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch1 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Level1GameManager.trigger1 = true;
    }

    
    void OnTriggerExit(Collider other)
    {
        Level1GameManager.trigger1 = false;
    }
}
