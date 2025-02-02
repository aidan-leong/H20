using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch5 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Level1GameManager.trigger5 = true;
    }

    
    void OnTriggerExit(Collider other)
    {
        Level1GameManager.trigger5 = false;
    }
}
