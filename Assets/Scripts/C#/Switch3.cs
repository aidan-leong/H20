using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch3 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Level1GameManager.trigger3 = true;
    }

    
    void OnTriggerExit(Collider other)
    {
        Level1GameManager.trigger3 = false;
    }
}
