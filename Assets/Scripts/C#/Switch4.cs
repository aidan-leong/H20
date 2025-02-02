using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch4 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Level1GameManager.trigger4 = true;
    }

    
    void OnTriggerExit(Collider other)
    {
        Level1GameManager.trigger4 = false;
    }
}
