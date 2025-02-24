using UnityEngine;

public class TriggerActivator : MonoBehaviour
{
    public GameObject targetObject; // The object to activate/deactivate
    public string playerTag = "Player"; // Tag of the player

    void Start()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false); // Ensure the object starts as inactive
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && targetObject != null)
        {
            targetObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }
}
