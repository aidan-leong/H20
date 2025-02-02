using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    // Public variable to specify the target object (the cube)
    public GameObject specifiedObject;

    // Public variable to specify the build index of the scene to load
    public int sceneBuildIndex;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the specified object
        if (other.gameObject == specifiedObject)
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
